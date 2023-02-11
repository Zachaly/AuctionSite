using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task AddOrderStocksAsync(IEnumerable<OrderStock> orderStocks, IEnumerable<int> stocksOnHoldIds);
        IEnumerable<T> GetOrdersByUserId<T>(string userId, Func<Order, T> selector);
        T GetOrderById<T>(int id, Func<Order, T> selector);
        IEnumerable<T> GetProductOrderStocks<T>(int productId, Func<OrderStock, T> selector);
        Task UpdateOrderStock(OrderStock stock);
        T GetOrderStockByIdAsync<T>(int id, Func<OrderStock, T> selector);
    }
}
