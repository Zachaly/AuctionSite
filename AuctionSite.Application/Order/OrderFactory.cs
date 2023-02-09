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
            => new Order
            {
                Address = request.Address,
                City = request.City,
                CreationDate = DateTime.Now,
                Email = request.Email,
                Name = request.Name,
                PaymentId = request.PaymentId,
                PhoneNumber = request.PhoneNumber,
                PostalCode = request.PostalCode,
                UserId = request.UserId,
            };

        public OrderManagementItem CreateManagementItem(OrderStock stock)
            => new OrderManagementItem
            {
                Status = stock.RealizationStatus,
                OrderStockId = stock.Id,
                Quantity = stock.Quantity,
                StockName = stock.Stock.Value
            };

        public OrderListItem CreateListItem(Order order)
            => new OrderListItem
            {
                Created = order.CreationDate.ToString("dd.MM.yyyy"),
                Id = order.Id,
            };

        public OrderModel CreateModel(Order order)
            => new OrderModel
            {
                Address = order.Address,
                City = order.City,
                Created = order.CreationDate.ToString("dd.MM.yyyy"),
                Id = order.Id,
                Items = order.Stocks.Select(stock => new OrderItem
                {
                    OrderStockId = stock.Id,
                    Price = stock.Stock.Product.Price,
                    ProductId = stock.Stock.ProductId,
                    ProductName = stock.Stock.Product.Name,
                    Quantity = stock.Quantity,
                    StockName = stock.Stock.Value,
                }),
                Name = order.Name,
                PaymentId = order.PaymentId,
                PhoneNumber = order.PhoneNumber,
                PostalCode = order.PostalCode,
            };

        public OrderProductModelModel CreateModel(OrderStock stock)
            => new OrderProductModelModel
            {
                Address = stock.Order.Address,
                City = stock.Order.City,
                CreationDate = stock.Order.CreationDate.ToString("dd.MM.yyyy"),
                Email = stock.Order.Email,
                Id = stock.Id,
                Name = stock.Order.Name,
                PaymentId = stock.Order.PaymentId,
                PhoneNumber = stock.Order.PhoneNumber,
                PostalCode = stock.Order.PostalCode,
                Quantity = stock.Quantity,
                Status = stock.RealizationStatus,
                StockId = stock.StockId,
                StockName = stock.Stock.Value
            };

        public OrderStock CreateStock(StockOnHold stockOnHold, int orderId)
            => new OrderStock
            {
                OrderId = orderId,
                Quantity = stockOnHold.Quantity,
                StockId = stockOnHold.StockId
            };
    }
}
