using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.Database.Repository
{
    [Implementation(typeof(IOrderRepository))]
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task AddOrderAsync(Order order)
        {
            _dbContext.Order.Add(order);

            return _dbContext.SaveChangesAsync();
        }

        public Task AddOrderStocksAsync(IEnumerable<OrderStock> orderStocks, IEnumerable<int> stocksOnHoldIds)
        {
            _dbContext.OrderStock.AddRange(orderStocks);

            var onHold = _dbContext.StockOnHold.Where(x => stocksOnHoldIds.Contains(x.Id));

            _dbContext.StockOnHold.RemoveRange(onHold);

            return _dbContext.SaveChangesAsync();
        }

        public T GetOrderById<T>(int id, Func<Order, T> selector)
            => _dbContext.Order
                .Include(order => order.Stocks)
                .ThenInclude(stock => stock.Stock)
                .ThenInclude(stock => stock.Product)
                .Where(order => order.Id == id)
                .Select(selector)
                .FirstOrDefault();


        public IEnumerable<T> GetOrdersByUserId<T>(string userId, Func<Order, T> selector)
            => _dbContext.Order.Where(order => order.UserId == userId).Select(selector);

        public T GetOrderStockByIdAsync<T>(int id, Func<OrderStock, T> selector)
            => _dbContext.OrderStock
                .Include(stock => stock.Stock)
                .Include(stock => stock.Order)
                .Where(stock => stock.Id == id)
                .Select(selector)
                .FirstOrDefault();


        public IEnumerable<T> GetProductOrderStocks<T>(int productId, Func<OrderStock, T> selector)
            => _dbContext.OrderStock
                .Include(stock => stock.Stock)
                .Include(stock => stock.Order)
                .Where(stock => stock.Stock.ProductId == productId)
                .Select(selector);

        public Task UpdateOrderStock(OrderStock stock)
        {
            _dbContext.OrderStock.Update(stock);

            return _dbContext.SaveChangesAsync();
        }
    }
}
