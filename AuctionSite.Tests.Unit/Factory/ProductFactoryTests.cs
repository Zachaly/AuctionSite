using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Product.Request;
using AuctionSite.Models.ProductOption;
using AuctionSite.Models.ProductOption.Request;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Unit.Factory
{
    public class ProductFactoryTests
    {
        private readonly Mock<IProductOptionFactory> _productOptionFactory;
        private readonly ProductFactory _productFactory;

        public ProductFactoryTests()
        {
            _productOptionFactory = new Mock<IProductOptionFactory>();

            _productFactory = new ProductFactory(_productOptionFactory.Object);
        }

        [Fact]
        public void Create()
        {
            _productOptionFactory.Setup(x => x.Create(It.IsAny<AddProductOptionRequest>()))
                .Returns(new ProductOption());

            var request = new AddProductRequest
            {
                Description = "description",
                Name = "name",
                OptionName = "optname",
                Options = new List<AddProductOptionRequest> { new AddProductOptionRequest { Value = "val", Quantity = 2 } },
                Price = 12.3M,
                UserId = "userId",
            };

            var product = _productFactory.Create(request);

            Assert.Equal(request.Description, product.Description);
            Assert.Equal(request.Name, product.Name);
            Assert.Equal(request.OptionName, product.OptionName);
            Assert.Equal(request.Price, product.Price);
            Assert.Equal(request.UserId, product.OwnerId);
            Assert.Single(product.Options);
        }

        [Fact]
        public void CreateModel()
        {
            _productOptionFactory.Setup(x => x.CreateModel(It.IsAny<ProductOption>()))
                .Returns(new ProductOptionModel());

            var product = new Product
            {
                Id = 1,
                Name = "name",
                Description = "description",
                OptionName = "optname",
                Options = new List<ProductOption>
                {
                    new ProductOption()
                },
                Owner = new ApplicationUser { UserName = "usrname" },
                OwnerId = "userId",
                Price = 200,
                Images = new List<ProductImage> 
                { 
                    new ProductImage() { Id = 10, },
                    new ProductImage() { Id = 11, },
                    new ProductImage() { Id = 12, }
                }
            };

            var model = _productFactory.CreateModel(product);

            Assert.Equal(product.Id, model.Id);
            Assert.Equal(product.Name, model.Name);
            Assert.Equal(product.Description, model.Description);
            Assert.Equal(product.OptionName, model.OptionName);
            Assert.Equal(product.Owner.UserName, model.UserName);
            Assert.Equal(product.OwnerId, model.UserId);
            Assert.Equal(product.Price.ToString(), model.Price.ToString());
            Assert.Single(product.Options);
            Assert.Equivalent(product.Images.Select(x => x.Id), model.ImageIds);
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
