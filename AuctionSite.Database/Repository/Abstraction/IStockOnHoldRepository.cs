using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IStockOnHoldRepository
    {
        Task AddStockOnHoldAsync(StockOnHold stockOnHold);
        Task DeleteStockOnHoldByIdAsync(int id);
        IEnumerable<T> GetStocksOnHoldByCartId<T>(int id, Func<StockOnHold, T> selector);
        T GetStockOnHoldById<T>(int id, Func<StockOnHold, T> selector);
    }
}
