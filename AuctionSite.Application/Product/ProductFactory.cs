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
            => new Product
            {
                Description = request.Description,
                Name = request.Name,
                OptionName = request.OptionName,
                Options = request.Options.Select(opt => _productOptionFactory.Create(opt)).ToArray(),
                OwnerId = request.UserId,
                Price = request.Price,
            };

        public ProductImage CreateImage(int productId, string name)
        {
            throw new NotImplementedException();
        }

        public ProductListItemModel CreateListItem(Product product)
            => new ProductListItemModel
            {
                Id = product.Id,
                Name = product.Name,
                ImageId = product.Images?.FirstOrDefault()?.Id ?? 0,
            };

        public ProductModel CreateModel(Product product)
            => new ProductModel
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                OptionName = product.OptionName,
                Options = product.Options.Select(opt => _productOptionFactory.CreateModel(opt)),
                Price = product.Price.ToString(),
                UserId = product.OwnerId,
                UserName = product.Owner.UserName,
                ImageIds = product.Images.Select(x => x.Id)
            };
    }
}
