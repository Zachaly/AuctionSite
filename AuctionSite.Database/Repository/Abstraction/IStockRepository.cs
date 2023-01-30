using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IStockRepository
    {
        Task AddStockAsync(Stock stock);
        Task DeleteStockByIdAsync(int id);
    }
}
