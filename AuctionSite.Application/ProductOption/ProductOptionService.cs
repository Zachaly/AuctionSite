using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.ProductOption.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application
{
    [Implementation(typeof(IProductOptionService))]
    public class ProductOptionService : IProductOptionService
    {
        private readonly IResponseFactory _responseFactory;
        private readonly IProductOptionFactory _productOptionFactory;
        private readonly IProductOptionRepository _productOptionRepository;

        public ProductOptionService(IResponseFactory responseFactory, IProductOptionFactory productOptionFactory,
            IProductOptionRepository productOptionRepository)
        {
            _responseFactory = responseFactory;
            _productOptionFactory = productOptionFactory;
            _productOptionRepository = productOptionRepository;
        }

        public Task<ResponseModel> AddProductOptionAsync(AddProductOptionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> DeleteProductOptionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
