using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Response;
using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuctionSite.Application
{
    [Implementation(typeof(IAuthService))]
    public class AuthService : IAuthService
    {
        private readonly IUserFactory _userFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IResponseFactory _responseFactory;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly string? _authAudience;
        private readonly string? _authIssuer;
        private readonly string? _secretKey;

        public AuthService(IUserFactory userFactory,
            UserManager<ApplicationUser> userManager,
            IResponseFactory responseFactory,
            IUserInfoRepository userInfoRepository,
            IConfiguration config)
        {
            _userFactory = userFactory;
            _userManager = userManager;
            _responseFactory = responseFactory;
            _userInfoRepository = userInfoRepository;

            _authAudience = config["Auth:Audience"];
            _authIssuer = config["Auth:Issuer"];
            _secretKey = config["Auth:SecretKey"];
        }

        public async Task<DataResponseModel<LoginResponse>> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if( user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return _responseFactory.CreateFailure<LoginResponse>("Username or password are incorrect");
            }

            var claims = await _userManager.GetClaimsAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));

            var token = new JwtSecurityToken(
                _authIssuer,
                _authAudience,
                claims,
                DateTime.Now,
                DateTime.Now.AddDays(365),
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)), SecurityAlgorithms.HmacSha256)
                );

            return _responseFactory.CreateSuccess(_userFactory.CreateLoginResponse(user, new JwtSecurityTokenHandler().WriteToken(token)));
        }

        public async Task<ResponseModel> Register(RegisterRequest request)
        {
            var user = _userFactory.Create(request);
            try
            {
                if(await _userManager.FindByEmailAsync(request.Email) is not null)
                {
                    return _responseFactory.CreateFailure("User with that email already exists!");
                }

                var res = await _userManager.CreateAsync(user, request.Password);

                if (!res.Succeeded)
                {
                    return _responseFactory.CreateFailure("Data is invalid");
                }

                var info = _userFactory.CreateInfo(request, user.Id);

                await _userInfoRepository.AddUserInfoAsync(info);

                return _responseFactory.CreateSuccess();
            }
            catch (Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }
    }
}
