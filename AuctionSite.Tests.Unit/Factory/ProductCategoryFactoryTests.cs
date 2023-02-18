using AuctionSite.Application;
using AuctionSite.Domain.Entity;

namespace AuctionSite.Tests.Unit.Factory
{
    public class ProductCategoryFactoryTests
    {
        private readonly ProductCategoryFactory _factory;

        public ProductCategoryFactoryTests()
        {
            _factory = new ProductCategoryFactory();
        }

        [Fact]
        public void CreateModel()
        {
            var category = new ProductCategory { Id = 1, Name = "cat" };

            var model = _factory.CreateModel(category);

            Assert.Equal(category.Id, model.Id);
            Assert.Equal(category.Name, model.Name);
        }
    }
}
