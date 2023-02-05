using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IListStockRepository
    {
        Task AddListStockAsync(ListStock stock);
        Task DeleteListByIdAsyncStock(int id);
    }
}
