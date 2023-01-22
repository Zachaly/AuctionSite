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
            => new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Username
            };

        public UserInfo CreateInfo(RegisterRequest request, string userId)
            => new UserInfo
            {
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                FirstName = request.FirstName,
                Gender = request.Gender,
                UserId = userId,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                PostalCode = request.PostalCode,
            };

        public LoginResponse CreateLoginResponse(ApplicationUser user, string token)
            => new LoginResponse
            {
                AuthToken = token,
                UserId = user.Id,
                UserName = user.UserName
            };
    }
}
