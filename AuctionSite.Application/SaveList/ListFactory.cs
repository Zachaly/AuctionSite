using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.SaveList;
using AuctionSite.Models.SaveList.Request;

namespace AuctionSite.Application
{
    [Implementation(typeof(IListFactory))]
    public class ListFactory : IListFactory
    {
        private readonly IListStockFactory _listStockFactory;

        public ListFactory(IListStockFactory listStockFactory)
        {
            _listStockFactory = listStockFactory;
        }

        public SaveList Create(AddListRequest request)
        {
            throw new NotImplementedException();
        }

        public ListListModel CreateListItem(SaveList list)
        {
            throw new NotImplementedException();
        }

        public ListModel CreateModel(SaveList list)
        {
            throw new NotImplementedException();
        }
    }
}
