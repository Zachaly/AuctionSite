using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Cart;
using AuctionSite.Models.Cart.Request;

namespace AuctionSite.Application
{
    [Implementation(typeof(IStockOnHoldFactory))]
    public class StockOnHoldFactory : IStockOnHoldFactory
    {
        public StockOnHold Create(AddToCartRequest request, int cartId)
        {
            throw new NotImplementedException();
        }

        public CartItem CreateCartItem(StockOnHold stock)
        {
            throw new NotImplementedException();
        }
    }
}
