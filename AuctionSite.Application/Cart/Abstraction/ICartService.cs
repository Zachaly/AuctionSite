using AuctionSite.Models.Cart;
using AuctionSite.Models.Cart.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application.Abstraction
{
    public interface ICartService
    {
        Task<ResponseModel> AddToCartAsync(AddToCartRequest request);
        Task<ResponseModel> AddCartAsync(AddCartRequest request);
        Task<ResponseModel> RemoveFromCartAsync(int stockId);
        DataResponseModel<int> GetCartItemsCount(string userId);
        DataResponseModel<CartModel> GetCartByUserId(string userId);
    }
}
