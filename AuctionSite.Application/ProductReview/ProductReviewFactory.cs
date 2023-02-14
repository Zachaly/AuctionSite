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
        {
            throw new NotImplementedException();
        }

        public ProductReviewListModel CreateModel(ProductReview review)
        {
            throw new NotImplementedException();
        }
    }
}
