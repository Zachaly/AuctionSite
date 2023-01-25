using AuctionSite.Domain.Entity;
using AuctionSite.Models.ProductOption.Request;
using Microsoft.IdentityModel.Tokens;

namespace AuctionSite.Tests.Integration
{
    public class ProductOptionControllerTests : IntegrationTest
    {
        private const string ApiPath = "/api/product-option";

        [Fact]
        public async Task PostAsync_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                OptionName = "opt",
                Options = new List<ProductOption>
                {
                    new ProductOption { ProductId = 1, Quantity = 1, Value = "value1" }
                },
                OwnerId = user.Id,
                Price = 12.34M
            };

            await AddToDatabase(new List<Product> { product });

            var request = new AddProductOptionRequest
            {
                ProductId = product.Id,
                Quantity = 12,
                Value = "value2"
            };

            var response = await _httpClient.PostAsJsonAsync(ApiPath, request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains(GetFromDatabase<ProductOption>(), x => x.Quantity == request.Quantity && x.Value == request.Value);
            Assert.Equal(2, GetFromDatabase<ProductOption>().Count());
        }

        [Fact]
        public async Task PostAsync_InvalidRequest_Fail()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                OptionName = "opt",
                Options = new List<ProductOption>
                {
                    new ProductOption { ProductId = 1, Quantity = 1, Value = "value1" }
                },
                OwnerId = user.Id,
                Price = 12.34M
            };

            await AddToDatabase(new List<Product> { product });

            var request = new AddProductOptionRequest
            {
                ProductId = product.Id,
            };

            var response = await _httpClient.PostAsJsonAsync(ApiPath, request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.DoesNotContain(GetFromDatabase<ProductOption>(), x => x.Quantity == request.Quantity && x.Value == request.Value);
            Assert.Single(GetFromDatabase<ProductOption>());
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                OptionName = "opt",
                Options = new List<ProductOption>
                {
                    new ProductOption { Id = 1, ProductId = 1, Quantity = 1, Value = "value1" },
                    new ProductOption { Id = 2, ProductId = 1, Quantity = 2, Value = "value2" },
                    new ProductOption { Id = 3, ProductId = 1, Quantity = 3, Value = "value3" },
                    new ProductOption { Id = 4, ProductId = 1, Quantity = 4, Value = "value4" }
                },
                OwnerId = user.Id,
                Price = 12.34M
            };

            await AddToDatabase(new List<Product> { product });

            const int Id = 3;

            var response = await _httpClient.DeleteAsync($"{ApiPath}/{Id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.DoesNotContain(GetFromDatabase<ProductOption>(), x => x.Id == Id);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_Fail()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                OptionName = "opt",
                Options = new List<ProductOption>
                {
                    new ProductOption { Id = 1, ProductId = 1, Quantity = 1, Value = "value1" },
                    new ProductOption { Id = 2, ProductId = 1, Quantity = 2, Value = "value2" },
                    new ProductOption { Id = 3, ProductId = 1, Quantity = 3, Value = "value3" },
                    new ProductOption { Id = 4, ProductId = 1, Quantity = 4, Value = "value4" }
                },
                OwnerId = user.Id,
                Price = 12.34M
            };

            await AddToDatabase(new List<Product> { product });

            const int Id = 2137;

            var response = await _httpClient.DeleteAsync($"{ApiPath}/{Id}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
