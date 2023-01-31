using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Cart;
using AuctionSite.Models.Cart.Request;

namespace AuctionSite.Application
{
    [Implementation(typeof(ICartFactory))]
    public class CartFactory : ICartFactory
    {
        private readonly IStockOnHoldFactory _stockOnHoldFactory;

        public CartFactory(IStockOnHoldFactory stockOnHoldFactory)
        {
            _stockOnHoldFactory = stockOnHoldFactory;
        }

        public Cart Create(AddCartRequest request)
            => new Cart 
            { 
                UserId = request.UserId 
            };

        public CartModel CreateModel(Cart cart)
        => new CartModel
        {
            Id = cart.Id,
            Items = cart.StocksOnHold.Select(x => _stockOnHoldFactory.CreateCartItem(x))
        };
    }
}
