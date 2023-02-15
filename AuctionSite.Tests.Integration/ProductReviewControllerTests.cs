using AuctionSite.Domain.Entity;
using AuctionSite.Models.ProductReview;
using AuctionSite.Models.ProductReview.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Tests.Integration
{
    public class ProductReviewControllerTests : IntegrationTest
    {
        private const string ApiUrl = "/api/product-review";

        [Fact]
        public async Task GetReviewsByProductId_Success()
        {
            var user = new ApplicationUser { Id = "id", UserName = "name" };

            await AddToDatabase(user);

            await AddToDatabase(new List<ProductReview>
            {
                new ProductReview { Id = 1, Content = "content", ProductId = 1, UserId = user.Id },
                new ProductReview { Id = 2, Content = "content", ProductId = 2, UserId = user.Id },
                new ProductReview { Id = 3, Content = "content", ProductId = 3, UserId = user.Id },
                new ProductReview { Id = 4, Content = "content", ProductId = 4, UserId = user.Id },
                new ProductReview { Id = 5, Content = "content", ProductId = 1, UserId = user.Id },
                new ProductReview { Id = 6, Content = "content", ProductId = 1, UserId = user.Id },
                new ProductReview { Id = 7, Content = "content", ProductId = 2, UserId = user.Id },
            });

            const int ProductId = 1;

            var response = await _httpClient.GetAsync($"{ApiUrl}/{ProductId}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<IEnumerable<ProductReviewListModel>>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equivalent(GetFromDatabase<ProductReview>().Where(x => x.ProductId == ProductId).Select(x => x.Id),
                content.Data.Select(x => x.Id));
        }

        [Fact]
        public async Task PostReviewAsync_Success()
        {
            await Authenticate();

            var userId = GetAuthenticatedUser().Id;

            var request = new AddProductReviewRequest
            {
                Content = "content",
                ProductId = 1,
                UserId = userId,
                Score = 10
            };

            var response = await _httpClient.PostAsJsonAsync(ApiUrl, request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains(GetFromDatabase<ProductReview>(), x => x.Score == request.Score);
        }

        [Fact]
        public async Task PostReviewAsync_ReviewExists_Fail()
        {
            await Authenticate();

            var userId = GetAuthenticatedUser().Id;

            var review = new ProductReview
            {
                ProductId = 1,
                UserId = userId,
                Score = 1,
                Content = "con"
            };

            await AddToDatabase(review);

            var request = new AddProductReviewRequest
            {
                Content = "content",
                ProductId = review.ProductId,
                UserId = review.UserId,
                Score = 10
            };

            var response = await _httpClient.PostAsJsonAsync(ApiUrl, request);
            var content = await response.Content.ReadFromJsonAsync<ResponseModel>();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEmpty(content.Error);
            Assert.DoesNotContain(GetFromDatabase<ProductReview>(), x => x.Score == request.Score);
        }

        [Fact]
        public async Task UpdateReviewAsync_FullRequest_Success()
        {
            await Authenticate();

            var review = new ProductReview
            {
                Id = 1,
                Content = "content",
                ProductId = 2,
                Score = 10,
                UserId = GetAuthenticatedUser().Id
            };

            await AddToDatabase(review);

            var request = new UpdateProductReviewRequest
            {
                Content = "new content",
                Score = 1,
                Id = review.Id,
            };

            var response = await _httpClient.PutAsJsonAsync(ApiUrl, request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Contains(GetFromDatabase<ProductReview>(), x => x.Id == request.Id && x.Score == request.Score && x.Content == request.Content);
        }

        [Fact]
        public async Task UpdateReviewAsync_BlankRequest_Success()
        {
            await Authenticate();

            var review = new ProductReview
            {
                Id = 1,
                Content = "content",
                ProductId = 2,
                Score = 10,
                UserId = GetAuthenticatedUser().Id
            };

            await AddToDatabase(review);

            var request = new UpdateProductReviewRequest
            {
                Content = null,
                Score = null,
                Id = review.Id,
            };

            var response = await _httpClient.PutAsJsonAsync(ApiUrl, request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.DoesNotContain(GetFromDatabase<ProductReview>(), x => x.Id == request.Id && x.Score == request.Score && x.Content == request.Content);
        }

        [Fact]
        public async Task UpdateReviewAsync_ReviewNotFound_Fail()
        {
            await Authenticate();

            var review = new ProductReview
            {
                Id = 1,
                Content = "content",
                ProductId = 2,
                Score = 10,
                UserId = GetAuthenticatedUser().Id
            };

            await AddToDatabase(review);

            var request = new UpdateProductReviewRequest
            {
                Content = "content",
                Score = 10,
                Id = 2137,
            };

            var response = await _httpClient.PutAsJsonAsync(ApiUrl, request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteReviewById_Success()
        {
            await Authenticate();

            await AddToDatabase(new List<ProductReview>
            {
                new ProductReview { Id = 1, Content = "content", ProductId = 1, UserId = "id" },
                new ProductReview { Id = 2, Content = "content", ProductId = 2, UserId = "id" },
                new ProductReview { Id = 3, Content = "content", ProductId = 3, UserId = "id" },
                new ProductReview { Id = 4, Content = "content", ProductId = 4, UserId = "id" },
                new ProductReview { Id = 5, Content = "content", ProductId = 1, UserId = "id" },
                new ProductReview { Id = 6, Content = "content", ProductId = 1, UserId = "id" },
                new ProductReview { Id = 7, Content = "content", ProductId = 2, UserId = "id" },
            });

            const int Id = 5;

            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{Id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.DoesNotContain(GetFromDatabase<ProductReview>(), x => x.Id == Id);
        }

        [Fact]
        public async Task DeleteReviewById_ReviewNotFound_Fail()
        {
            await Authenticate();

            await AddToDatabase(new List<ProductReview>
            {
                new ProductReview { Id = 1, Content = "content", ProductId = 1, UserId = "id" },
                new ProductReview { Id = 2, Content = "content", ProductId = 2, UserId = "id" },
                new ProductReview { Id = 3, Content = "content", ProductId = 3, UserId = "id" },
                new ProductReview { Id = 4, Content = "content", ProductId = 4, UserId = "id" },
                new ProductReview { Id = 5, Content = "content", ProductId = 1, UserId = "id" },
                new ProductReview { Id = 6, Content = "content", ProductId = 1, UserId = "id" },
                new ProductReview { Id = 7, Content = "content", ProductId = 2, UserId = "id" },
            });

            const int Id = 2137;

            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{Id}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
