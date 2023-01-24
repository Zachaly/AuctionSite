using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Product;
using AuctionSite.Models.Product.Request;

namespace AuctionSite.Application
{
    [Implementation(typeof(IProductFactory))]
    public class ProductFactory : IProductFactory
    {
        private readonly IProductOptionFactory _productOptionFactory;

        public ProductFactory(IProductOptionFactory productOptionFactory)
        {
            _productOptionFactory = productOptionFactory;
        }

        public Product Create(AddProductRequest request)
        {
            throw new NotImplementedException();
        }

        public ProductListItemModel CreateListItem(Product product)
        {
            throw new NotImplementedException();
        }

        public ProductModel CreateModel(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
