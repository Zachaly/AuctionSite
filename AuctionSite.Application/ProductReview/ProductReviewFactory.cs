using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using AuctionSite.Models.ProductReview;
using AuctionSite.Models.ProductReview.Request;

namespace AuctionSite.Application
{
    [Implementation(typeof(IProductReviewFactory))]
    public class ProductReviewFactory : IProductReviewFactory
    {
        public ProductReview Create(AddProductReviewRequest request)
            => new ProductReview
            {
                ProductId = request.ProductId,
                UserId = request.UserId,
                Score = request.Score,
                Content = request.Content,
            };

        public ProductReviewListModel CreateModel(ProductReview review)
            => new ProductReviewListModel
            {
                Content = review.Content,
                Id = review.Id,
                Score = review.Score,
                UserId = review.UserId,
                UserName = review.User.UserName
            };
    }
}
