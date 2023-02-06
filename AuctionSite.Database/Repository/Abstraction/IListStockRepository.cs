using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IListStockRepository
    {
        Task AddListStockAsync(ListStock stock);
        Task DeleteListStokcByIdAsync(int id);
    }
}
