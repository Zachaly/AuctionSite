using AuctionSite.Domain.Entity;
using AuctionSite.Models.ListStock;
using AuctionSite.Models.ListStock.Request;

namespace AuctionSite.Application.Abstraction
{
    public interface IListStockFactory
    {
        ListStock Create(AddListStockRequest request);
        ListStockModel CreateModel(AddListStockRequest request);
    }
}
