using AuctionSite.Domain.Entity;
using AuctionSite.Models.Product;
using AuctionSite.Models.Product.Request;

namespace AuctionSite.Application.Abstraction
{
    public interface IProductFactory
    {
        Product Create(AddProductRequest request);
        ProductListItemModel CreateListItem(Product product);
        ProductModel CreateModel(Product product);
        ProductImage CreateImage(int productId, string name);
    }
}
