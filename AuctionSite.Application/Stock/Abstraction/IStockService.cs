using AuctionSite.Models.Stock.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application.Abstraction
{
    public interface IStockService
    {
        Task<ResponseModel> AddStockAsync(AddStockRequest request);
        Task<ResponseModel> DeleteStockByIdAsync(int id);
    }
}
