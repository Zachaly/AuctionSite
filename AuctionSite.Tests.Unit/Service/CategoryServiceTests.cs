using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Category;
using Moq;

namespace AuctionSite.Tests.Unit.Service
{
    public class CategoryServiceTests : ServiceTest
    {
        private readonly Mock<IProductCategoryFactory> _productCategoryFactory;
        private readonly Mock<IProductCategoryRepository> _productCategoryRepository;
        private readonly ProductCategoryService _service;

        public CategoryServiceTests() : base()
        {
            _productCategoryFactory = new Mock<IProductCategoryFactory>();
            _productCategoryRepository = new Mock<IProductCategoryRepository>();

            _service = new ProductCategoryService(_responseFactory.Object, _productCategoryRepository.Object, _productCategoryFactory.Object);
        }

        [Fact]
        public async Task GetCategoriesAsync()
        {
            var categories = new List<ProductCategory>
            {
                new ProductCategory { Id = 1 },
                new ProductCategory { Id = 2 },
                new ProductCategory { Id = 3 },
                new ProductCategory { Id = 4 },
                new ProductCategory { Id = 5 },
            };

            _productCategoryRepository.Setup(x => x.GetProductCategories(It.IsAny<Func<ProductCategory, CategoryModel>>()))
                .Returns((Func<ProductCategory, CategoryModel> selector) => categories.Select(selector));

            _productCategoryFactory.Setup(x => x.CreateModel(It.IsAny<ProductCategory>()))
                .Returns((ProductCategory category) => new CategoryModel { Id = category.Id });

            MockDataResponse<IEnumerable<CategoryModel>>();

            var res = await _service.GetCategoriesAsync();

            Assert.True(res.Success);
            Assert.Equivalent(categories.Select(x => x.Id), res.Data.Select(x => x.Id));
        }
    }
}
