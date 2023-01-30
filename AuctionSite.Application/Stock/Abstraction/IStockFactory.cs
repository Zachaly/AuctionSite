using AuctionSite.Domain.Entity;
using AuctionSite.Models.Stock;
using AuctionSite.Models.Stock.Request;

namespace AuctionSite.Application.Abstraction
{
    public interface IStockFactory
    {
        Stock Create(AddStockRequest request);
        StockModel CreateModel(Stock stock);
    }
}
