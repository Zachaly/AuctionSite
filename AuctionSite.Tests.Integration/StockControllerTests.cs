using AuctionSite.Domain.Entity;
using AuctionSite.Models.Stock.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Tests.Integration
{
    public class StockControllerTests : IntegrationTest
    {
        private const string ApiPath = "/api/stock";

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
                StockName = "opt",
                Stocks = new List<Stock>
                {
                    new Stock { ProductId = 1, Quantity = 1, Value = "value1" }
                },
                OwnerId = user.Id,
                Price = 12.34M
            };

            await AddToDatabase(product);

            var request = new AddStockRequest
            {
                ProductId = product.Id,
                Quantity = 12,
                Value = "value2"
            };

            var response = await _httpClient.PostAsJsonAsync(ApiPath, request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains(GetFromDatabase<Stock>(), x => x.Quantity == request.Quantity && x.Value == request.Value);
            Assert.Equal(2, GetFromDatabase<Stock>().Count());
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
                StockName = "opt",
                Stocks = new List<Stock>
                {
                    new Stock { ProductId = 1, Quantity = 1, Value = "value1" }
                },
                OwnerId = user.Id,
                Price = 12.34M
            };

            await AddToDatabase(product);

            var request = new AddStockRequest
            {
                ProductId = product.Id,
                Quantity = -1,
                Value = "val"
            };

            var response = await _httpClient.PostAsJsonAsync(ApiPath, request);
            var error = await response.Content.ReadFromJsonAsync<ResponseModel>();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.DoesNotContain(GetFromDatabase<Stock>(), x => x.Quantity == request.Quantity && x.Value == request.Value);
            Assert.Single(GetFromDatabase<Stock>());
            Assert.Contains(error.ValidationErrors.Keys, x => x == "Quantity");
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
                StockName = "opt",
                Stocks = new List<Stock>
                {
                    new Stock { Id = 1, ProductId = 1, Quantity = 1, Value = "value1" },
                    new Stock { Id = 2, ProductId = 1, Quantity = 2, Value = "value2" },
                    new Stock { Id = 3, ProductId = 1, Quantity = 3, Value = "value3" },
                    new Stock { Id = 4, ProductId = 1, Quantity = 4, Value = "value4" }
                },
                OwnerId = user.Id,
                Price = 12.34M
            };

            await AddToDatabase(product);

            const int Id = 3;

            var response = await _httpClient.DeleteAsync($"{ApiPath}/{Id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.DoesNotContain(GetFromDatabase<Stock>(), x => x.Id == Id);
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
                StockName = "opt",
                Stocks = new List<Stock>
                {
                    new Stock { Id = 1, ProductId = 1, Quantity = 1, Value = "value1" },
                    new Stock { Id = 2, ProductId = 1, Quantity = 2, Value = "value2" },
                    new Stock { Id = 3, ProductId = 1, Quantity = 3, Value = "value3" },
                    new Stock { Id = 4, ProductId = 1, Quantity = 4, Value = "value4" }
                },
                OwnerId = user.Id,
                Price = 12.34M
            };

            await AddToDatabase(product);

            const int Id = 2137;

            var response = await _httpClient.DeleteAsync($"{ApiPath}/{Id}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
