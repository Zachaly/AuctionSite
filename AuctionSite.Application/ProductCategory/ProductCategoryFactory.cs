using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Category;

namespace AuctionSite.Application
{
    [Implementation(typeof(IProductCategoryFactory))]
    public class ProductCategoryFactory : IProductCategoryFactory
    {
        public CategoryModel CreateModel(ProductCategory category)
        {
            throw new NotImplementedException();
        }
    }
}
