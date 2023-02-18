using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Product.Request;
using AuctionSite.Models.Stock;
using AuctionSite.Models.Stock.Request;
using Moq;

namespace AuctionSite.Tests.Unit.Factory
{
    public class ProductFactoryTests
    {
        private readonly Mock<IStockFactory> _stockFactory;
        private readonly ProductFactory _productFactory;

        public ProductFactoryTests()
        {
            _stockFactory = new Mock<IStockFactory>();

            _productFactory = new ProductFactory(_stockFactory.Object);
        }

        [Fact]
        public void Create()
        {
            _stockFactory.Setup(x => x.Create(It.IsAny<AddStockRequest>()))
                .Returns(new Stock());

            var request = new AddProductRequest
            {
                Description = "description",
                Name = "name",
                StockName = "optname",
                Stocks = new List<AddStockRequest> { new AddStockRequest { Value = "val", Quantity = 2 } },
                Price = 12.3M,
                UserId = "userId",
                CategoryId = 1
            };

            var product = _productFactory.Create(request);

            Assert.Equal(request.Description, product.Description);
            Assert.Equal(request.Name, product.Name);
            Assert.Equal(request.StockName, product.StockName);
            Assert.Equal(request.Price, product.Price);
            Assert.Equal(request.UserId, product.OwnerId);
            Assert.Single(product.Stocks);
            Assert.Equal(request.CategoryId, product.CategoryId);
            Assert.NotEqual(default, product.Created);
        }

        [Fact]
        public void CreateModel()
        {
            _stockFactory.Setup(x => x.CreateModel(It.IsAny<Stock>()))
                .Returns(new StockModel());

            var product = new Product
            {
                Id = 1,
                Name = "name",
                Description = "description",
                StockName = "optname",
                Stocks = new List<Stock>
                {
                    new Stock()
                },
                Owner = new ApplicationUser { UserName = "usrname" },
                OwnerId = "userId",
                Price = 200,
                Images = new List<ProductImage> 
                { 
                    new ProductImage() { Id = 10, },
                    new ProductImage() { Id = 11, },
                    new ProductImage() { Id = 12, }
                },
                Created = DateTime.Now,
                CategoryId = 2,
                Category = new ProductCategory { Id = 2, Name = "cat" }
            };

            var model = _productFactory.CreateModel(product);

            Assert.Equal(product.Id, model.Id);
            Assert.Equal(product.Name, model.Name);
            Assert.Equal(product.Description, model.Description);
            Assert.Equal(product.StockName, model.StockName);
            Assert.Equal(product.Owner.UserName, model.UserName);
            Assert.Equal(product.OwnerId, model.UserId);
            Assert.Equal(product.Price.ToString(), model.Price.ToString());
            Assert.Single(product.Stocks);
            Assert.Equivalent(product.Images.Select(x => x.Id), model.ImageIds);
            Assert.NotEmpty(model.Created);
            Assert.Equal(product.CategoryId, model.CategoryId);
            Assert.Equal(product.Category.Name, model.CategoryName);
        }

        [Fact]
        public void CreateListItem()
        {
            var product = new Product { Id = 1, Name = "name", Images = new List<ProductImage> { new ProductImage { Id = 2 } } };

            var item = _productFactory.CreateListItem(product);

            Assert.Equal(product.Id, item.Id);
            Assert.Equal(product.Name, item.Name);
            Assert.Equal(product.Images.First().Id, item.ImageId);
        }

        [Fact]
        public void CreateImage()
        {
            const int Id = 1;
            const string File = "file";
            var image = _productFactory.CreateImage(Id, File);

            Assert.Equal(Id, image.ProductId);
            Assert.Equal(File, image.FileName);
        }
    }
}
