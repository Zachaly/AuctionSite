using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using Microsoft.EntityFrameworkCore;

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
            _dbContext.SaveList.Add(list);

            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteListByIdAsync(int id)
        {
            _dbContext.SaveList.Remove(_dbContext.SaveList.Find(id));

            return _dbContext.SaveChangesAsync();
        }

        public T GetListById<T>(int id, Func<SaveList, T> selector)
            => _dbContext.SaveList
                .Include(list => list.Stocks)
                .ThenInclude(stock => stock.Stock)
                .ThenInclude(stock => stock.Product)
                .Where(list => list.Id == id)
                .Select(selector).FirstOrDefault();

        public IEnumerable<T> GetUserLists<T>(string userId, Func<SaveList, T> selector)
            => _dbContext.SaveList
                .Where(list => list.UserId == userId)
                .Select(selector);
    }
}
