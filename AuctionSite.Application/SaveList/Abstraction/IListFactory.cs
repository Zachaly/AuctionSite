using AuctionSite.Domain.Entity;
using AuctionSite.Models.SaveList;
using AuctionSite.Models.SaveList.Request;

namespace AuctionSite.Application.Abstraction
{
    public interface IListFactory
    {
        SaveList Create(AddListRequest request);
        ListListModel CreateListItem(SaveList list);
        ListModel CreateModel(SaveList list);
    }
}
