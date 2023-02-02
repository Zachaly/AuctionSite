using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;

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
            throw new NotImplementedException();
        }

        public Task AddOrderStocksAsync(IEnumerable<OrderStock> orderStocks, IEnumerable<int> stocksOnHoldIds)
        {
            throw new NotImplementedException();
        }

        public T GetOrderById<T>(int id, Func<Order, T> selector)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetOrdersByUserId<T>(string userId, Func<Order, T> selector)
        {
            throw new NotImplementedException();
        }
    }
}
