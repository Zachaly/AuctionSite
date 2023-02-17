using AuctionSite.Application;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.ProductReview.Request;

namespace AuctionSite.Tests.Unit.Factory
{
    public class ProductReviewFactoryTests
    {
        private readonly ProductReviewFactory _factory;

        public ProductReviewFactoryTests()
        {
            _factory = new ProductReviewFactory();
        }

        [Fact]
        public void Create()
        {
            var request = new AddProductReviewRequest
            {
                ProductId = 1,
                Content = "content",
                Score = 10,
                UserId = "id"
            };

            var review = _factory.Create(request);

            Assert.Equal(request.ProductId, review.ProductId);
            Assert.Equal(request.UserId, review.UserId);
            Assert.Equal(request.Score, review.Score);
            Assert.Equal(request.Content, review.Content);
        }

        [Fact]
        public void CreateModel()
        {
            var review = new ProductReview
            {
                ProductId = 1,
                Content = "content",
                Score = 10,
                Id = 2,
                UserId = "id",
                User = new ApplicationUser
                {
                    UserName = "name",
                    Id = "id"
                }
            };

            var model = _factory.CreateModel(review);

            Assert.Equal(review.Id, model.Id);
            Assert.Equal(review.Content, model.Content);
            Assert.Equal(review.Score, model.Score);
            Assert.Equal(review.UserId, model.UserId);
            Assert.Equal(review.User.UserName, model.UserName);
        }
    }
}
