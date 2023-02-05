using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using Microsoft.EntityFrameworkCore;

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
            var stock = _dbContext.Stock.Find(stockOnHold.StockId);

            if(stock.Quantity - stockOnHold.Quantity < 0)
            {
                return Task.CompletedTask;
            }

            stock.Quantity -= stockOnHold.Quantity;
            _dbContext.Stock.Update(stock);

            _dbContext.StockOnHold.Add(stockOnHold);

            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteStockOnHoldByIdAsync(int id)
        {
            var stockOnHold = _dbContext.StockOnHold.Find(id);
            var stock = _dbContext.Stock.Find(stockOnHold.StockId);

            stock.Quantity += stockOnHold.Quantity;
            _dbContext.Stock.Update(stock);

            _dbContext.StockOnHold.Remove(stockOnHold);

            return _dbContext.SaveChangesAsync();
        }

        public T GetStockOnHoldById<T>(int id, Func<StockOnHold, T> selector)
            => _dbContext.StockOnHold
                .Include(stock => stock.Stock)
                .ThenInclude(stock => stock.Product)
                .Where(stock => stock.Id == id)
                .Select(selector)
                .FirstOrDefault();

        public IEnumerable<T> GetStocksOnHoldByCartId<T>(int id, Func<StockOnHold, T> selector)
            => _dbContext.StockOnHold
                .Include(stock => stock.Stock)
                .ThenInclude(stock => stock.Product)
                .Where(stock => stock.CartId == id)
                .Select(selector);
    }
}
