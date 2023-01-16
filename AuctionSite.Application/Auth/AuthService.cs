using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Response;
using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Response;
using Microsoft.AspNetCore.Identity;

namespace AuctionSite.Application
{
    [Implementation(typeof(IAuthService))]
    public class AuthService : IAuthService
    {
        private readonly IUserFactory _userFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IResponseFactory _responseFactory;

        public AuthService(IUserFactory userFactory, UserManager<ApplicationUser> userManager, IResponseFactory responseFactory)
        {
            _userFactory = userFactory;
            _userManager = userManager;
            _responseFactory = responseFactory;
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
