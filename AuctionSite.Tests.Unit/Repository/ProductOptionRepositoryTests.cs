using AuctionSite.Database.Repository;
using AuctionSite.Domain.Entity;

namespace AuctionSite.Tests.Unit.Repository
{
    public class ProductOptionRepositoryTests : DatabaseTest
    {
        private readonly ProductOptionRepository _repository;

        public ProductOptionRepositoryTests() : base()
        {
            _repository = new ProductOptionRepository(_dbContext);
        }

        [Fact]
        public async Task AddProductOptionAsync()
        {
            var option = new ProductOption
            {
                ProductId = 1,
                Quantity = 2,
                Value = "val"
            };

            await _repository.AddProductOptionAsync(option);

            Assert.Contains(_dbContext.ProductOption, x => x.ProductId == option.Id);
        }

        [Fact]
        public async Task DeleteProductOptionByIdAsync()
        {
            AddContent(new List<ProductOption>
            {
                new ProductOption { Id = 1, Quantity = 2, ProductId = 3, Value = "val" },
                new ProductOption { Id = 2, Quantity = 2, ProductId = 3, Value = "val" },
                new ProductOption { Id = 3, Quantity = 2, ProductId = 3, Value = "val" },
                new ProductOption { Id = 4, Quantity = 2, ProductId = 3, Value = "val" },
            });

            const int Id = 3;

            await _repository.DeleteProductOptionByIdAsync(Id);

            Assert.DoesNotContain(_dbContext.ProductOption, x => x.Id == Id);
        }
    }
}
