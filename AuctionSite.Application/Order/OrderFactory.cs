using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Order;
using AuctionSite.Models.Order.Request;

namespace AuctionSite.Application
{
    [Implementation(typeof(IOrderFactory))]
    public class OrderFactory : IOrderFactory
    {
        public Order Create(AddOrderRequest request)
        {
            throw new NotImplementedException();
        }

        public OrderListItem CreateListItem(Order order)
        {
            throw new NotImplementedException();
        }

        public OrderModel CreateModel(Order order)
        {
            throw new NotImplementedException();
        }

        public OrderStock CreateStock(StockOnHold stockOnHold, int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
