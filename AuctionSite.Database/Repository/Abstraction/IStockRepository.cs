using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IStockRepository
    {
        Task AddStockAsync(Stock stock);
        Task DeleteStockByIdAsync(int id);
        Task UpdateStockAsync(Stock stock);
        T GetStockById<T>(int id, Func<Stock, T> selector);
    }
}
