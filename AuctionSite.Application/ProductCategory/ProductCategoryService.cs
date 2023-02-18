using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Category;
using AuctionSite.Models.Response;

namespace AuctionSite.Application
{
    [Implementation(typeof(IProductCategoryService))]
    public class ProductCategoryService : IProductCategoryService
    {
        public Task<DataResponseModel<IEnumerable<CategoryModel>>> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
