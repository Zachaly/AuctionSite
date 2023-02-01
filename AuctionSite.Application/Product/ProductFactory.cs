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
        private readonly IStockFactory _stockFactory;

        public ProductFactory(IStockFactory stockFactory)
        {
            _stockFactory = stockFactory;
        }

        public Product Create(AddProductRequest request)
            => new Product
            {
                Description = request.Description,
                Name = request.Name,
                StockName = request.StockName,
                Stocks = request.Stocks.Select(opt => _stockFactory.Create(opt)).ToArray(),
                OwnerId = request.UserId,
                Price = request.Price,
                Created = DateTime.Now,
            };

        public ProductImage CreateImage(int productId, string name)
            => new ProductImage
            {
                ProductId = productId,
                FileName = name,
            };

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
                StockName = product.StockName,
                Stocks = product.Stocks.Select(opt => _stockFactory.CreateModel(opt)),
                Price = product.Price.ToString(),
                UserId = product.OwnerId,
                UserName = product.Owner.UserName,
                ImageIds = product.Images.Select(x => x.Id),
                Created = product.Created.ToString("dd.MM.yyyy")
            };
    }
}
