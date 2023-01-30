using AuctionSite.Models;
using AuctionSite.Models.Product;
using AuctionSite.Models.Product.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application.Abstraction
{
    public interface IProductService
    {
        Task<ResponseModel> AddProductAsync(AddProductRequest request);
        Task<ResponseModel> DeleteProductByIdAsync(int id);
        DataResponseModel<ProductModel> GetProductById(int id);
        DataResponseModel<IEnumerable<ProductListItemModel>> GetProducts(PagedRequest request);
        DataResponseModel<int> GetPageCount(GetPageCountRequest request);
    }
}
