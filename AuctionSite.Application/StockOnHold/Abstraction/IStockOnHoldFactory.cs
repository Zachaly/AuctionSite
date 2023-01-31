using AuctionSite.Domain.Entity;
using AuctionSite.Models.Cart;
using AuctionSite.Models.Cart.Request;

namespace AuctionSite.Application.Abstraction
{
    public interface IStockOnHoldFactory
    {
        StockOnHold Create(AddToCartRequest request, int cartId);
        CartItem CreateCartItem(StockOnHold stock);
    }
}
