using AuctionSite.Domain.Entity;
using AuctionSite.Models.ProductOption;
using AuctionSite.Models.ProductOption.Request;

namespace AuctionSite.Application.Abstraction
{
    public interface IProductOptionFactory
    {
        ProductOption Create(AddProductOptionRequest request);
        ProductOption Create(AddProductOptionRequest request, int productId);
        ProductOptionModel CreateModel(ProductOption option);
    }
}
