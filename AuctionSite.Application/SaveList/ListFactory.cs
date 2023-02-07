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
            => new SaveList
            {
                Name = request.Name,
                UserId = request.UserId,
            };

        public ListListModel CreateListItem(SaveList list)
            => new ListListModel
            {
                Id = list.Id,
                Name = list.Name,
            };

        public ListModel CreateModel(SaveList list)
            => new ListModel 
            { 
                Id = list.Id,
                Name = list.Name,
                Items = list.Stocks.Select(stock => _listStockFactory.CreateModel(stock))
            };
    }
}
