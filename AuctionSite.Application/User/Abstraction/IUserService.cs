using AuctionSite.Models.Response;
using AuctionSite.Models.User;
using AuctionSite.Models.User.Request;

namespace AuctionSite.Application.Abstraction
{
    public interface IUserService
    {
        Task<DataResponseModel<UserProfileModel>> GetUserByIdAsync(string id);
        Task<ResponseModel> UpdateUserAsync(UpdateUserRequest request);
    }
}
