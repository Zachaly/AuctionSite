using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuctionSite.Tests.Unit.Service
{
    public class AuthServiceTests : ServiceTest
    {
        private readonly AuthService _authService;
        private readonly Mock<UserManager<ApplicationUser>> _userManager;
        private readonly Mock<IUserFactory> _userFactory;
        private readonly Mock<IUserInfoRepository> _userInfoRepository;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
        private readonly Mock<IConfiguration> _configuration;

        public AuthServiceTests() : base()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            _userManager.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            _userManager.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            _userFactory = new Mock<IUserFactory>();
            _userInfoRepository = new Mock<IUserInfoRepository>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Auth:Audience")]).Returns("https://localhost");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Auth:Issuer")]).Returns("https://localhost");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Auth:SecretKey")]).Returns("supersecretkeyloooooooooooooooooooooooooooooooong");

            _authService = new AuthService(_userFactory.Object, _userManager.Object, _responseFactory.Object,
                _userInfoRepository.Object, _configuration.Object, _httpContextAccessor.Object);
        }

        [Fact]
        public async Task Register_Success()
        {
            var users = new List<ApplicationUser>();
            var infos = new List<UserInfo>();

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => users.FirstOrDefault(x => x.Email == email));

            _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Callback((ApplicationUser user, string _) => 
                { 
                    user.Id = Guid.NewGuid().ToString();
                    users.Add(user);
                }).ReturnsAsync(IdentityResult.Success);

            _userInfoRepository.Setup(x => x.AddUserInfoAsync(It.IsAny<UserInfo>()))
                .Callback((UserInfo info) =>
                {
                    infos.Add(info);
                });

            _userFactory.Setup(x => x.Create(It.IsAny<RegisterRequest>()))
                .Returns((RegisterRequest registerRequest) => new ApplicationUser
                {
                    Email = registerRequest.Email,
                    UserName = registerRequest.Username
                });

            _userFactory.Setup(x => x.CreateInfo(It.IsAny<RegisterRequest>(), It.IsAny<string>()))
                .Returns((RegisterRequest request, string id) => new UserInfo
                { 
                    UserId = id,
                    FirstName = request.FirstName,
                });

            var request = new RegisterRequest
            {
                Username = "username",
                Email = "email@email.com",
                Password = "password",
                FirstName = "fname"
            };

            var response = await _authService.Register(request);

            Assert.True(response.Success);
            Assert.Contains(users, x => x.UserName == request.Username);
            Assert.Contains(infos, x => x.FirstName == request.FirstName 
                && x.UserId == users.First(y => y.UserName == request.Username).Id);
        }

        [Fact]
        public async Task Register_UserExists_Fail()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "id", Email = "email@email.com" }
            };
            var infos = new List<UserInfo>();

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => users.FirstOrDefault(x => x.Email == email));

            _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userFactory.Setup(x => x.Create(It.IsAny<RegisterRequest>()))
                .Returns((RegisterRequest registerRequest) => new ApplicationUser
                {
                    Email = registerRequest.Email,
                    UserName = registerRequest.Username
                });

            _userFactory.Setup(x => x.CreateInfo(It.IsAny<RegisterRequest>(), It.IsAny<string>()))
                .Returns((RegisterRequest request, string id) => new UserInfo
                {
                    UserId = id,
                    FirstName = request.FirstName,
                });

            var request = new RegisterRequest
            {
                Username = "username",
                Email = "email@email.com",
                Password = "password",
                FirstName = "fname"
            };

            var response = await _authService.Register(request);

            Assert.False(response.Success);
        }

        [Fact]
        public async Task Register_ExceptionThrown_Fail()
        {
            var users = new List<ApplicationUser>();
            var infos = new List<UserInfo>();

            const string ExceptionMessage = "ex message";

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => users.FirstOrDefault(x => x.Email == email));

            _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Callback((ApplicationUser user, string _) =>
                {
                    throw new Exception(ExceptionMessage);
                }).ReturnsAsync(IdentityResult.Success);

            _userFactory.Setup(x => x.Create(It.IsAny<RegisterRequest>()))
                .Returns((RegisterRequest registerRequest) => new ApplicationUser
                {
                    Email = registerRequest.Email,
                    UserName = registerRequest.Username
                });

            _userFactory.Setup(x => x.CreateInfo(It.IsAny<RegisterRequest>(), It.IsAny<string>()))
                .Returns((RegisterRequest request, string id) => new UserInfo
                {
                    UserId = id,
                    FirstName = request.FirstName,
                });

            var request = new RegisterRequest
            {
                Username = "username",
                Email = "email@email.com",
                Password = "password",
                FirstName = "fname"
            };

            var response = await _authService.Register(request);

            Assert.False(response.Success);
            Assert.DoesNotContain(users, x => x.UserName == request.Username);
            Assert.DoesNotContain(infos, x => x.FirstName == request.FirstName
                && x.UserId == users.First(y => y.UserName == request.Username).Id);
            Assert.Equal(ExceptionMessage, response.Error);
        }

        [Fact]
        public async Task Register_FailedToAddUser_Fail()
        {
            var users = new List<ApplicationUser>();
            var infos = new List<UserInfo>();

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => users.FirstOrDefault(x => x.Email == email));

            _userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            _userFactory.Setup(x => x.Create(It.IsAny<RegisterRequest>()))
                .Returns((RegisterRequest registerRequest) => new ApplicationUser
                {
                    Email = registerRequest.Email,
                    UserName = registerRequest.Username
                });

            _userFactory.Setup(x => x.CreateInfo(It.IsAny<RegisterRequest>(), It.IsAny<string>()))
                .Returns((RegisterRequest request, string id) => new UserInfo
                {
                    UserId = id,
                    FirstName = request.FirstName,
                });

            var request = new RegisterRequest
            {
                Username = "username",
                Email = "email@email.com",
                Password = "password",
                FirstName = "fname"
            };

            var response = await _authService.Register(request);

            Assert.False(response.Success);
            Assert.DoesNotContain(users, x => x.UserName == request.Username);
            Assert.DoesNotContain(infos, x => x.FirstName == request.FirstName
                && x.UserId == users.First(y => y.UserName == request.Username).Id);
        }

        [Fact]
        public async Task Login_Success()
        {
            var user = new ApplicationUser { Id = "id2", Email = "email@email.com2" };

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "id1", Email = "email@email.com1" },
                user,
                new ApplicationUser { Id = "id3", Email = "email@email.com3" },
                new ApplicationUser { Id = "id4", Email = "email@email.com4" },
                new ApplicationUser { Id = "id5", Email = "email@email.com5" }
            };

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => users.FirstOrDefault(x => x.Email == email));

            _userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).
                ReturnsAsync(true);

            _userManager.Setup(x => x.GetClaimsAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<Claim>());

            _userFactory.Setup(x => x.CreateLoginResponse(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns((ApplicationUser user, string token) => new LoginResponse
                {
                    AuthToken = token,
                    UserId = user.Id,
                    UserName = user.UserName
                });

            MockDataResponse<LoginResponse>();

            var request = new LoginRequest
            {
                Email = user.Email,
                Password = "pass"
            };

            var response = await _authService.Login(request);

            var tokenValidation = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Object["Auth:SecretKey"])),
                ValidIssuer = _configuration.Object["Auth:Issuer"],
                ValidAudience = _configuration.Object["Auth:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var validationResult = tokenHandler.ValidateToken(response.Data.AuthToken, tokenValidation, out SecurityToken validToken);

            Assert.True(response.Success);
            Assert.Equal(user.Id, response.Data.UserId);
            Assert.Equal(user.UserName, response.Data.UserName);
            Assert.Contains(validationResult.Identities.First().Claims, x => x.Value == user.Id);
        }

        [Fact]
        public async Task Login_UserNotFound_Fail()
        {
            var user = new ApplicationUser { Id = "id2", Email = "email@email.com2" };

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "id1", Email = "email@email.com1" },
                user,
                new ApplicationUser { Id = "id3", Email = "email@email.com3" },
                new ApplicationUser { Id = "id4", Email = "email@email.com4" },
                new ApplicationUser { Id = "id5", Email = "email@email.com5" }
            };

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => null);

            _userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).
                ReturnsAsync(true);

            _userManager.Setup(x => x.GetClaimsAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<Claim>());

            _userFactory.Setup(x => x.CreateLoginResponse(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns((ApplicationUser user, string token) => new LoginResponse
                {
                    AuthToken = token,
                    UserId = user.Id,
                    UserName = user.UserName
                });

            MockDataResponse<LoginResponse>();

            var request = new LoginRequest
            {
                Email = user.Email,
                Password = "pass"
            };

            var response = await _authService.Login(request);

            Assert.False(response.Success);
            Assert.NotNull(response.Error);
        }

        [Fact]
        public async Task Login_PasswordIncorrect_Fail()
        {
            var user = new ApplicationUser { Id = "id2", Email = "email@email.com2" };

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "id1", Email = "email@email.com1" },
                user,
                new ApplicationUser { Id = "id3", Email = "email@email.com3" },
                new ApplicationUser { Id = "id4", Email = "email@email.com4" },
                new ApplicationUser { Id = "id5", Email = "email@email.com5" }
            };

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => users.FirstOrDefault(x => x.Email == email));

            _userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).
                ReturnsAsync(false);

            _userManager.Setup(x => x.GetClaimsAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<Claim>());

            _userFactory.Setup(x => x.CreateLoginResponse(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns((ApplicationUser user, string token) => new LoginResponse
                {
                    AuthToken = token,
                    UserId = user.Id,
                    UserName = user.UserName
                });

            MockDataResponse<LoginResponse>();

            var request = new LoginRequest
            {
                Email = user.Email,
                Password = "pass"
            };

            var response = await _authService.Login(request);

            Assert.False(response.Success);
            Assert.NotNull(response.Error);
        }

        [Fact]
        public async Task GetUserDataAsync_Success()
        {
            _httpContextAccessor.Setup(x => x.HttpContext.User)
                .Returns(new ClaimsPrincipal());

            const string Token = "aasdasdasdasdasd";
            var headersMock = new Mock<IHeaderDictionary>();
            headersMock.SetupGet(x => x[It.Is<string>(s => s == HeaderNames.Authorization)]).Returns($"Bearer {Token}");

            _httpContextAccessor.Setup(x => x.HttpContext.Request.Headers)
                .Returns(headersMock.Object);

            var user = new ApplicationUser { Id = "id", UserName = "username" };

            _userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            _userFactory.Setup(x => x.CreateLoginResponse(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns((ApplicationUser user, string token) => new LoginResponse { AuthToken = token, UserId = user.Id, UserName = user.UserName });

            MockDataResponse<LoginResponse>();

            var result = await _authService.GetCurrentUserDataAsync();

            Assert.True(result.Success);
            Assert.Equal(Token, result.Data.AuthToken);
            Assert.Equal(user.UserName, result.Data.UserName);
            Assert.Equal(user.Id, result.Data.UserId);
        }

        [Fact]
        public async Task GetUserDataAsync_ExceptionThrown_Fail()
        {
            _httpContextAccessor.Setup(x => x.HttpContext.User)
                .Returns(new ClaimsPrincipal());

            const string Token = "aasdasdasdasdasd";
            var headersMock = new Mock<IHeaderDictionary>();
            headersMock.SetupGet(x => x[It.Is<string>(s => s == HeaderNames.Authorization)]).Returns($"Bearer {Token}");

            _httpContextAccessor.Setup(x => x.HttpContext.Request.Headers)
                .Returns(headersMock.Object);

            var user = new ApplicationUser { Id = "id", UserName = "username" };

            const string Error = "error";
            _userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .Callback(() =>
                {
                    throw new Exception(Error);
                });

            _userFactory.Setup(x => x.CreateLoginResponse(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns((ApplicationUser user, string token) => new LoginResponse { AuthToken = token, UserId = user.Id, UserName = user.UserName });

            MockDataResponse<LoginResponse>();

            var result = await _authService.GetCurrentUserDataAsync();

            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(Error, result.Error);
        }
    }
}
