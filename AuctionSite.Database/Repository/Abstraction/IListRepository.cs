using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IListRepository
    {
        T GetListById<T>(int id, Func<SaveList, T> selector);
        IEnumerable<T> GetUserLists<T>(string userId, Func<SaveList, T> selector);
        Task AddListAsync(SaveList list);
        Task DeleteListByIdAsync(int id);
    }
}
