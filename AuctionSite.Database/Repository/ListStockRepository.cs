using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;

namespace AuctionSite.Database.Repository
{
    [Implementation(typeof(IListStockRepository))]
    public class ListStockRepository : IListStockRepository
    {
        private readonly AppDbContext _dbContext;

        public ListStockRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddListStockAsync(ListStock stock)
        {
            _dbContext.ListStock.Add(stock);

            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteListStokcByIdAsync(int id)
        {
            _dbContext.ListStock.Remove(_dbContext.ListStock.Find(id));

            return _dbContext.SaveChangesAsync();
        }
    }
}
