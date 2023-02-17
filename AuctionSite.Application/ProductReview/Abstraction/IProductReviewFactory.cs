using AuctionSite.Domain.Entity;
using AuctionSite.Models.ProductReview.Request;
using AuctionSite.Models.ProductReview;

namespace AuctionSite.Application.Abstraction
{
    public interface IProductReviewFactory
    {
        ProductReview Create(AddProductReviewRequest request);
        ProductReviewListModel CreateModel(ProductReview review);
    }
}
