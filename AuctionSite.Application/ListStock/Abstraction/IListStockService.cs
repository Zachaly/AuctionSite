using AuctionSite.Models.ListStock.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application.Abstraction
{
    public interface IListStockService
    {
        Task<ResponseModel> AddListStockAsync(AddListStockRequest request);
        Task<ResponseModel> DeleteListStockByIdAsync(int id);
    }
}
