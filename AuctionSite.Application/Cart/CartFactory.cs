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
        {
            throw new NotImplementedException();
        }

        public CartModel CreateModel(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
