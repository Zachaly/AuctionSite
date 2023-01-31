using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;

namespace AuctionSite.Database.Repository
{
    [Implementation(typeof(IStockOnHoldRepository))]
    public class StockOnHoldRepository : IStockOnHoldRepository
    {
        private readonly AppDbContext _dbContext;

        public StockOnHoldRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddStockOnHoldAsync(StockOnHold stockOnHold)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStockOnHoldByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public T GetStockOnHoldById<T>(int id, Func<StockOnHold, T> selector)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetStocksOnHoldByCartId<T>(int id, Func<StockOnHold, T> selector)
        {
            throw new NotImplementedException();
        }
    }
}
