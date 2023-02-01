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
            => new StockOnHold
            {
                CartId = cartId,
                Quantity = request.Quantity,
                StockId = request.StockId,
            };

        public CartItem CreateCartItem(StockOnHold stock)
            => new CartItem
            {
                Quantity = stock.Quantity,
                StockOnHoldId = stock.Id,
                Value = stock.Stock.Value,
                ProductId = stock.Stock.ProductId,
                ProductName = stock.Stock.Product.Name,
                Price = stock.Quantity * stock.Stock.Product.Price
            };
    }
}
