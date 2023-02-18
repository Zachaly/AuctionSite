using AuctionSite.Models.Category;
using AuctionSite.Models.Response;

namespace AuctionSite.Application.Abstraction
{
    public interface IProductCategoryService
    {
        Task<DataResponseModel<IEnumerable<CategoryModel>>> GetCategoriesAsync();
    }
}
