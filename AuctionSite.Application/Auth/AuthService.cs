using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Response;
using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

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

        public Task<DataResponseModel<LoginResponse>> Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
