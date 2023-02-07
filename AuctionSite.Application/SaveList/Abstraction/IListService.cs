using AuctionSite.Models.Response;
using AuctionSite.Models.SaveList;
using AuctionSite.Models.SaveList.Request;

namespace AuctionSite.Application.Abstraction
{
    public interface IListService
    {
        Task<ResponseModel> AddListAsync(AddListRequest request);
        Task<ResponseModel> DeleteListByIdAsync(int id);
        DataResponseModel<ListModel> GetListById(int id);
        DataResponseModel<IEnumerable<ListListModel>> GetUserLists(string userId);
    }
}
