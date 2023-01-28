using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models;
using AuctionSite.Models.Product;
using AuctionSite.Models.Product.Request;
using AuctionSite.Models.Product.Validator;
using AuctionSite.Models.ProductOption.Validator;
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

        public async Task<ResponseModel> AddProductAsync(AddProductRequest request)
        {
            var validation = new AddProductRequestValidator(new AddProductOptionRequestValidator()).Validate(request);

            if(!validation.IsValid)
            {
                return _responseFactory.CreateValidationError(validation);
            }

            try
            {
                var product = _productFactory.Create(request);

                await _productRepository.AddProductAsync(product);

                return _responseFactory.CreateSuccess();
            }
            catch (Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }

        public async Task<ResponseModel> DeleteProductByIdAsync(int id)
        {
            try
            {
                await _productRepository.DeleteProductByIdAsync(id);

                return _responseFactory.CreateSuccess();
            }
            catch (Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }

        public DataResponseModel<ProductModel> GetProductById(int id)
        {
            try
            {
                var product = _productRepository.GetProductById(id, prod => _productFactory.CreateModel(prod));

                if(product is null)
                {
                    return _responseFactory.CreateFailure<ProductModel>("Product nor found");
                }

                return _responseFactory.CreateSuccess(product);
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure<ProductModel>(ex.Message);
            }
        }

        public DataResponseModel<IEnumerable<ProductListItemModel>> GetProducts(PagedRequest request)
        {
            var index = request.PageIndex ?? 0;
            var pageSize = request.PageSize ?? 10;

            try
            {
                var data = _productRepository.GetProducts(index, pageSize, product => _productFactory.CreateListItem(product));

                return _responseFactory.CreateSuccess(data);
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure<IEnumerable<ProductListItemModel>>(ex.Message);
            }
        }
    }
}
