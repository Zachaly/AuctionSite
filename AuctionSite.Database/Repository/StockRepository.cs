using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;

namespace AuctionSite.Database.Repository
{
    [Implementation(typeof(IStockRepository))]
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _dbContext;

        public StockRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddStockAsync(Stock stock)
        {
            _dbContext.Stock.AddAsync(stock);

            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteStockByIdAsync(int id)
        {
            _dbContext.Stock.Remove(_dbContext.Stock.Find(id));

            return _dbContext.SaveChangesAsync();
        }

        public T GetStockById<T>(int id, Func<Stock, T> selector)
            => _dbContext.Stock
                .Where(stock => stock.Id == id)
                .Select(selector).FirstOrDefault();

        public Task UpdateStockAsync(Stock stock)
        {
            _dbContext.Stock.Update(stock);

            return _dbContext.SaveChangesAsync();
        }
    }
}
