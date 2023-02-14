using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository
{
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

        public T GetProductReviewByProductAndUserId<T>(int productId, int userId, Func<ProductReview, T> selector)
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
