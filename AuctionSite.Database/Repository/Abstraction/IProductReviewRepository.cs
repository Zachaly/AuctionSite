using AuctionSite.Domain.Entity;

namespace AuctionSite.Database.Repository.Abstraction
{
    public interface IProductReviewRepository
    {
        Task AddReviewAsync(ProductReview review);
        Task DeleteReviewByIdAsync(int id);
        Task UpdateReviewAsync(ProductReview review);
        IEnumerable<T> GetReviewsByProductId<T>(int productId, Func<ProductReview, T> selector);
        T GetProductReviewByProductAndUserId<T>(int productId, int userId, Func<ProductReview, T> selector);
    }
}
