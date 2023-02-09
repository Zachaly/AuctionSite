using AuctionSite.Domain.Entity;
using AuctionSite.Models.Order;
using AuctionSite.Models.Order.Request;

namespace AuctionSite.Application.Abstraction
{
    public interface IOrderFactory
    {
        OrderStock CreateStock(StockOnHold stockOnHold, int orderId);
        Order Create(AddOrderRequest request);
        OrderListItem CreateListItem(Order order);
        OrderModel CreateModel(Order order);
        ProductOrderModel CreateModel(OrderStock stock);
    }
}
