using AuctionSite.Domain.Entity;
using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Response;

namespace AuctionSite.Application.Abstraction
{
    public interface IUserFactory
    {
        UserInfo CreateInfo(RegisterRequest request, string userId);
        ApplicationUser Create(RegisterRequest request);
        LoginResponse CreateLoginResponse(ApplicationUser user, string token);
    }
}
