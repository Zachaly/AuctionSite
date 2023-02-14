using AuctionSite.Database.Repository;
using AuctionSite.Domain.Entity;
using System.Security.Cryptography.X509Certificates;

namespace AuctionSite.Tests.Unit.Repository
{
    public class ProductReviewRepositoryTests : DatabaseTest
    {
        private readonly ProductReviewRepository _repository;

        public ProductReviewRepositoryTests()
        {
            _repository = new ProductReviewRepository(_dbContext);
        }

        [Fact]
        public async Task AddReviewAsync()
        {
            var review = new ProductReview
            {
                Content = "content",
                Score = 10,
                ProductId = 2,
                UserId = "id"
            };

            await _repository.AddReviewAsync(review);

            Assert.Contains(_dbContext.ProductReview, x => x.Content == review.Content);
        }

        [Fact]
        public async Task DeleteReviewByIdAsync()
        {
            AddContent(new List<ProductReview>
            {
                new ProductReview { Id = 1, Content = "content", UserId = "id" },
                new ProductReview { Id = 2, Content = "content", UserId = "id" },
                new ProductReview { Id = 3, Content = "content", UserId = "id" },
                new ProductReview { Id = 4, Content = "content", UserId = "id" },
            });

            const int Id = 3;

            await _repository.DeleteReviewByIdAsync(Id);

            Assert.DoesNotContain(_dbContext.ProductReview, x => x.Id == Id);
        }

        [Fact]
        public void GetProductReviewByProductAndUserId()
        {

            var review = new ProductReview { Id = 3, Content = "content_user", UserId = "id", ProductId = 2 };

            AddContent(new List<ProductReview>
            {
                new ProductReview { Id = 1, Content = "content", UserId = "id", ProductId = 1 },
                new ProductReview { Id = 2, Content = "content", UserId = "id2", ProductId = 2 },
                review,
                new ProductReview { Id = 4, Content = "content", UserId = "id2", ProductId = 1 },
            });

            var res = _repository.GetProductReviewByProductAndUserId(review.ProductId, review.UserId, x => x);

            Assert.NotNull(res);
            Assert.Equal(review.Content, res.Content);
        }

        [Fact]
        public void GetReviewsByProductId()
        {
            AddContent(new List<ApplicationUser>
            {
                new ApplicationUser { Id = "id" },
                new ApplicationUser { Id = "id2" },
            });

            var reviews = new List<ProductReview>
            {
                new ProductReview { Id = 1, Content = "content", UserId = "id", ProductId = 1 },
                new ProductReview { Id = 2, Content = "content", UserId = "id2", ProductId = 2 },
                new ProductReview { Id = 3, Content = "content", UserId = "id", ProductId = 3 },
                new ProductReview { Id = 5, Content = "content", UserId = "id2", ProductId = 3 },
                new ProductReview { Id = 6, Content = "content", UserId = "id2", ProductId = 4 },
                new ProductReview { Id = 7, Content = "content", UserId = "id2", ProductId = 3 },
            };

            AddContent(reviews);

            const int ProductId = 3;

            var res = _repository.GetReviewsByProductId(ProductId, x => x);

            Assert.Equivalent(reviews.Where(x => x.ProductId == ProductId).Select(x => x.Id), res.Select(x => x.Id));
        }

        [Fact]
        public async Task UpdateReviewAsync()
        {
            var review = new ProductReview { Id = 1, Content = "content", UserId = "id" };

            AddContent(review);

            review.Content = "new content";

            await _repository.UpdateReviewAsync(review);

            Assert.Contains(_dbContext.ProductReview, x => x.Id == review.Id && x.Content == review.Content);
        }

        [Fact]
        public void GetProductReviewById()
        {
            AddContent(new List<ProductReview>
            {
                new ProductReview { Id = 1, Content = "content1", UserId = "id" },
                new ProductReview { Id = 2, Content = "content2", UserId = "id" },
                new ProductReview { Id = 3, Content = "content3", UserId = "id" },
                new ProductReview { Id = 4, Content = "content4", UserId = "id" },
            });

            const int Id = 3;

            var res = _repository.GetReviewById(Id, x => x);

            Assert.Equal(_dbContext.ProductReview.First(x => x.Id == Id).Content, res.Content);
        }
    }
}
