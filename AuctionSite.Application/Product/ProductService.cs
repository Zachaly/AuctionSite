using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models;
using AuctionSite.Models.Product;
using AuctionSite.Models.Product.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application
{
    [Implementation(typeof(IProductService))]
    public class ProductService : IProductService
    {
        private readonly IResponseFactory _responseFactory;
        private readonly IProductFactory _productFactory;
        private readonly IProductRepository _productRepository;

        public ProductService(IResponseFactory responseFactory, IProductFactory productFactory, IProductRepository productRepository)
        {
            _responseFactory = responseFactory;
            _productFactory = productFactory;
            _productRepository = productRepository;
        }

        public Task<ResponseModel> AddProductAsync(AddProductRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> DeleteProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public DataResponseModel<ProductModel> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public DataResponseModel<IEnumerable<ProductListItemModel>> GetProducts(PagedRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
