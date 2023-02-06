using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository
{
    public class ListStockRepository : IListStockRepository
    {
        private readonly AppDbContext _dbContext;

        public ListStockRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddListStockAsync(ListStock stock)
        {
            throw new NotImplementedException();
        }

        public Task DeleteListByIdAsyncStock(int id)
        {
            throw new NotImplementedException();
        }
    }
}
