using AuctionSite.Models.ProductReview;
using AuctionSite.Models.ProductReview.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application.Abstraction
{
    public interface IProductReviewService
    {
        Task<ResponseModel> AddProductReviewAsync(AddProductReviewRequest request);
        Task<ResponseModel> UpdateProductReviewAsync(UpdateProductReviewRequest request);
        Task<ResponseModel> DeleteProductReviewAsync(int id);
        Task<DataResponseModel<IEnumerable<ProductReviewListModel>>> GetProductReviewsAsync(int id);
    }
}
