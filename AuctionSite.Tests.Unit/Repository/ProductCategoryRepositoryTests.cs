using AuctionSite.Database.Repository;
using AuctionSite.Domain.Entity;

namespace AuctionSite.Tests.Unit.Repository
{
    public class ProductCategoryRepositoryTests : DatabaseTest
    {
        private readonly ProductCategoryRepository _repository;

        public ProductCategoryRepositoryTests() : base()
        {
            _repository = new ProductCategoryRepository(_dbContext);
        }

        [Fact]
        public void GetCategories()
        {
            AddContent(new List<ProductCategory>()
            {
                new ProductCategory { Id = 1, Name = "name1" },
                new ProductCategory { Id = 2, Name = "name2" },
                new ProductCategory { Id = 3, Name = "name3" },
                new ProductCategory { Id = 4, Name = "name4" },
                new ProductCategory { Id = 5, Name = "name5" },
            });

            var res = _repository.GetProductCategories(x => x);

            Assert.Equal(_dbContext.ProductCategory.Count(), res.Count());
        }
    }
}
