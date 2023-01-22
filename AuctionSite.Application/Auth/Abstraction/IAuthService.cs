using AuctionSite.Models.Response;
using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Response;

namespace AuctionSite.Application.Abstraction
{
    public interface IAuthService
    {
        Task<DataResponseModel<LoginResponse>> Login(LoginRequest request);
        Task<ResponseModel> Register(RegisterRequest request); 
    }
}
