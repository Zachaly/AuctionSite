using AuctionSite.Application;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.ProductOption.Request;

namespace AuctionSite.Tests.Unit.Factory
{
    public class ProductOptionFactoryTests
    {
        [Fact]
        public void Create_WithProductId()
        {
            var factory = new ProductOptionFactory();

            var request = new AddProductOptionRequest
            {
                Quantity = 1,
                Value = "val"
            };

            const int Id = 2;

            var option = factory.Create(request, Id);

            Assert.Equal(request.Value, option.Value);
            Assert.Equal(request.Quantity, option.Quantity);
            Assert.Equal(Id, option.Id);
        }

        [Fact]
        public void Create()
        {
            var factory = new ProductOptionFactory();

            var request = new AddProductOptionRequest
            {
                ProductId = 1,
                Quantity = 2,
                Value = "val"
            };

            var option = factory.Create(request);

            Assert.Equal(request.ProductId, option.ProductId);
            Assert.Equal(request.Value, option.Value);
            Assert.Equal(request.Quantity, option.Quantity);
        }

        [Fact]
        public void CreateModel()
        {
            var factory = new ProductOptionFactory();

            var option = new ProductOption
            {
                Id = 1,
                Quantity = 2,
                Value = "val"
            };

            var model = factory.CreateModel(option);

            Assert.Equal(option.Id, model.Id);
            Assert.Equal(option.Quantity, model.Quantity);
            Assert.Equal(option.Value, model.Value);
        }

        [Fact]
        public void CreateWithoutProductId()
        {
            var factory = new ProductOptionFactory();

            var request = new AddProductOptionRequest
            {
                Quantity = 1,
                Value = "val"
            };

            var option = factory.CreateWithoutProductId(request);

            Assert.Equal(request.Value, option.Value);
            Assert.Equal(request.Quantity, option.Quantity);
        }
    }
}
