using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.ProductReview;
using AuctionSite.Models.ProductReview.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application
{
    [Implementation(typeof(IProductReviewService))]
    public class ProductReviewService : IProductReviewService
    {
        private readonly IProductReviewRepository _productReviewRepository;
        private readonly IProductReviewFactory _productReviewFactory;
        private readonly IResponseFactory _responseFactory;

        public ProductReviewService(IProductReviewRepository productReviewRepository, IProductReviewFactory productReviewFactory,
            IResponseFactory responseFactory)
        {
            _productReviewRepository = productReviewRepository;
            _productReviewFactory = productReviewFactory;
            _responseFactory = responseFactory;
        }
        public Task<ResponseModel> AddProductReviewAsync(AddProductReviewRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> DeleteProductReviewAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponseModel<IEnumerable<ProductReviewListModel>>> GetProductReviewsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> UpdateProductReviewAsync(UpdateProductReviewRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
