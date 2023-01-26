using AuctionSite.Models.ProductOption.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application.Abstraction
{
    public interface IProductOptionService
    {
        Task<ResponseModel> AddProductOptionAsync(AddProductOptionRequest request);
        Task<ResponseModel> DeleteProductOptionByIdAsync(int id);
    }
}
