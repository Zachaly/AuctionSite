using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository
{
    public class ListRepository : IListRepository
    {
        private readonly AppDbContext _dbContext;

        public ListRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddListAsync(SaveList list)
        {
            throw new NotImplementedException();
        }

        public Task DeleteListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public T GetListById<T>(int id, Func<SaveList, T> selector)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetUserLists<T>(string userId, Func<SaveList, T> selector)
        {
            throw new NotImplementedException();
        }
    }
}
