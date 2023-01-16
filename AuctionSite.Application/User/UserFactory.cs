using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Response;

namespace AuctionSite.Application
{
    [Implementation(typeof(IUserFactory))]
    public class UserFactory : IUserFactory
    {
        public ApplicationUser Create(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public UserInfo CreateInfo(RegisterRequest request, string userId)
        {
            throw new NotImplementedException();
        }

        public LoginResponse CreateLoginResponse(ApplicationUser user, string token)
        {
            throw new NotImplementedException();
        }
    }
}
