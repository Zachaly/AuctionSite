using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;

namespace AuctionSite.Database.Repository
{
    [Implementation(typeof(IProductReviewRepository))]
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductReviewRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddReviewAsync(ProductReview review)
        {
            throw new NotImplementedException();
        }

        public Task DeleteReviewByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public T GetProductReviewByProductAndUserId<T>(int productId, string userId, Func<ProductReview, T> selector)
        {
            throw new NotImplementedException();
        }

        public T GetReviewById<T>(int id, Func<ProductReview, T> selector)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetReviewsByProductId<T>(int productId, Func<ProductReview, T> selector)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReviewAsync(ProductReview review)
        {
            throw new NotImplementedException();
        }
    }
}
