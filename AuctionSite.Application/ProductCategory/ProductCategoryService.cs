using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Category;
using AuctionSite.Models.Response;

namespace AuctionSite.Application
{
    [Implementation(typeof(IProductCategoryService))]
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IResponseFactory _responseFactory;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductCategoryFactory _productCategoryFactory;

        public ProductCategoryService(IResponseFactory responseFactory, IProductCategoryRepository productCategoryRepository,
            IProductCategoryFactory productCategoryFactory)
        {
            _responseFactory = responseFactory;
            _productCategoryRepository = productCategoryRepository;
            _productCategoryFactory = productCategoryFactory;
        }

        public async Task<DataResponseModel<IEnumerable<CategoryModel>>> GetCategoriesAsync()
        {
            var data = _productCategoryRepository.GetProductCategories(category => _productCategoryFactory.CreateModel(category));

            return _responseFactory.CreateSuccess(data);
        }
    }
}
