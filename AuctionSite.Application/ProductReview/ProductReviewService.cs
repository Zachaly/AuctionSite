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
        public async Task<ResponseModel> AddProductReviewAsync(AddProductReviewRequest request)
        {
            try
            {
                if(_productReviewRepository.GetProductReviewByProductAndUserId(request.ProductId, request.UserId, x => x) is not null)
                {
                    return _responseFactory.CreateFailure("Review exists");
                }

                var review = _productReviewFactory.Create(request);

                await _productReviewRepository.AddReviewAsync(review);

                return _responseFactory.CreateSuccess();
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }

        public async Task<ResponseModel> DeleteProductReviewAsync(int id)
        {
            try
            {
                await _productReviewRepository.DeleteReviewByIdAsync(id);

                return _responseFactory.CreateSuccess();
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }

        public async Task<DataResponseModel<IEnumerable<ProductReviewListModel>>> GetProductReviewsAsync(int id)
        {
            var data = _productReviewRepository.GetReviewsByProductId(id, review => _productReviewFactory.CreateModel(review));

            return _responseFactory.CreateSuccess(data);
        }

        public async Task<ResponseModel> UpdateProductReviewAsync(UpdateProductReviewRequest request)
        {
            try
            {
                var review = _productReviewRepository.GetReviewById(request.Id, review => review);

                review.Score = request.Score ?? review.Score;
                review.Content = request.Content ?? review.Content;

                await _productReviewRepository.UpdateReviewAsync(review);

                return _responseFactory.CreateSuccess();
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure(ex.Message);
            }
        }
    }
}
