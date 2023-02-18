using AuctionSite.Domain.Entity;
using AuctionSite.Models.Category;

namespace AuctionSite.Application.Abstraction
{
    public interface IProductCategoryFactory
    {
        CategoryModel CreateModel(ProductCategory category);
    }
}
