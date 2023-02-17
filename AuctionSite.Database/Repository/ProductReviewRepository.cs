using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Util;
using Microsoft.EntityFrameworkCore;

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
            _dbContext.ProductReview.Add(review);

            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteReviewByIdAsync(int id)
        {
            var review = _dbContext.ProductReview.FirstOrDefault(x => x.Id == id);

            _dbContext.ProductReview.Remove(review);

            return _dbContext.SaveChangesAsync();
        }

        public T GetProductReviewByProductAndUserId<T>(int productId, string userId, Func<ProductReview, T> selector)
            => _dbContext.ProductReview
                .Where(review => review.ProductId == productId && review.UserId == userId)
                .Select(selector)
                .FirstOrDefault();

        public T GetReviewById<T>(int id, Func<ProductReview, T> selector)
            => _dbContext.ProductReview
                .Where(review => review.Id == id)
                .Select(selector)
                .FirstOrDefault();

        public IEnumerable<T> GetReviewsByProductId<T>(int productId, Func<ProductReview, T> selector)
            => _dbContext.ProductReview
                .Include(review => review.User)
                .Where(review => review.ProductId == productId)
                .Select(selector);

        public Task UpdateReviewAsync(ProductReview review)
        {
            _dbContext.ProductReview.Update(review);

            return _dbContext.SaveChangesAsync();
        }
    }
}
