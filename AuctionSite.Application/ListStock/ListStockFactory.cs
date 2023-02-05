using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.ListStock;
using AuctionSite.Models.ListStock.Request;

namespace AuctionSite.Application
{
    [Implementation(typeof(IListStockFactory))]
    public class ListStockFactory : IListStockFactory
    {
        public ListStock Create(AddListStockRequest request)
        {
            throw new NotImplementedException();
        }

        public ListStockModel CreateModel(AddListStockRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
