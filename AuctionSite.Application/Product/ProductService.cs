using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models;
using AuctionSite.Models.Product;
using AuctionSite.Models.Product.Request;
using AuctionSite.Models.Product.Validator;
using AuctionSite.Models.Stock.Validator;
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

        private (int Index, int Size) GetIndexAndSize(PagedRequest request) => (request.PageIndex ?? 0, request.PageSize ?? 10);

        public async Task<ResponseModel> AddProductAsync(AddProductRequest request)
        {
            var validation = new AddProductRequestValidator(new AddStockRequestValidator()).Validate(request);

            if(!validation.IsValid)
            {
                return _responseFactory.CreateValidationError(validation);
            }

            try
            {
                var product = _productFactory.Create(request);

                await _productRepository.AddProductAsync(product);

                return _responseFactory.CreateSuccessWithCreatedId(product.Id);
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

        public DataResponseModel<int> GetPageCount(GetPageCountRequest request)
        {
            var count = 0;

            if (request.CategoryId is not null)
            {
                count = _productRepository.GetCategoryPageCount(request.CategoryId.Value, request.PageSize ?? 10);
            }
            else
            {
                count = request.UserId is not null ?
                    _productRepository.GetUserPageCount(request.UserId, request.PageSize ?? 10) :
                    _productRepository.GetPageCount(request.PageSize ?? 10);
            }

            return _responseFactory.CreateSuccess(count);
        }

        public DataResponseModel<ProductModel> GetProductById(int id)
        {
            try
            {
                var product = _productRepository.GetProductById(id, prod => _productFactory.CreateModel(prod));

                if(product is null)
                {
                    return _responseFactory.CreateFailure<ProductModel>("Product not found");
                }

                return _responseFactory.CreateSuccess(product);
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure<ProductModel>(ex.Message);
            }
        }

        public DataResponseModel<IEnumerable<ProductListItemModel>> GetProducts(GetProductsRequest request)
        {
            var index = GetIndexAndSize(request);

            try
            {
                IEnumerable<ProductListItemModel> data;
                if(request.UserId is not null)
                {
                    data = _productRepository.GetProductsByUserId(request.UserId, index.Size, index.Index, product => _productFactory.CreateListItem(product));
                }
                else if(request.CategoryId is not null)
                {
                    data = _productRepository.GetProductsByCategoryId(request.CategoryId.Value, index.Index, index.Size, product => _productFactory.CreateListItem(product));
                }
                else
                {
                    data = _productRepository.GetProducts(index.Index, index.Size, product => _productFactory.CreateListItem(product));
                }
                return _responseFactory.CreateSuccess(data);
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure<IEnumerable<ProductListItemModel>>(ex.Message);
            }
        }

        public DataResponseModel<FoundProductsModel> SearchProducts(GetProductsRequest request)
        {
            var index = GetIndexAndSize(request);

            var products = _productRepository.SearchProducts(request.CategoryId, request.Name,
                index.Index, index.Size, product => _productFactory.CreateListItem(product));

            var count = _productRepository.GetPageCount(request.CategoryId, request.Name, index.Size);

            var data = _productFactory.CreateFoundProducts(products, count);

            return _responseFactory.CreateSuccess(data);
        }

        public async Task<ResponseModel> UpdateProductAsync(UpdateProductRequest request)
        {
            try
            {
                var validation = new UpdateProductRequestValidator().Validate(request);

                if(!validation.IsValid)
                {
                    return _responseFactory.CreateValidationError(validation);
                }

                var product = _productRepository.GetProductById(request.Id, prod => prod);

                product.Name = request.Name ?? product.Name;
                product.Description = request.Description ?? product.Description;
                product.Price = request.Price ?? product.Price;
                product.StockName = request.StockName ?? product.StockName;
                product.CategoryId = request.CategoryId ?? product.CategoryId;

                await _productRepository.UpdateProductAsync(product);

                return _responseFactory.CreateSuccess();
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }
    }
}
