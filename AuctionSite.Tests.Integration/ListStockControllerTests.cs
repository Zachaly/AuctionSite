using AuctionSite.Domain.Entity;
using AuctionSite.Models.ListStock.Request;

namespace AuctionSite.Tests.Integration
{
    public class ListStockControllerTests : IntegrationTest
    {
        private const string ApiUrl = "/api/list-stock";

        [Fact]
        public async Task PostListStockAsync_Success()
        {
            await Authenticate();

            var request = new AddListStockRequest
            {
                ListId = 1,
                Quantity = 2,
                StockId = 3,
            };

            var response = await _httpClient.PostAsJsonAsync(ApiUrl, request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains(GetFromDatabase<ListStock>(), x => x.Quantity == request.Quantity);
        }

        [Fact]
        public async Task DeleteListStockAsync_Success()
        {
            await Authenticate();

            await AddToDatabase(new List<ListStock>
            {
                new ListStock { Id = 1, ListId = 2 , Quantity = 3, StockId = 4 },
                new ListStock { Id = 2, ListId = 2 , Quantity = 3, StockId = 4 },
                new ListStock { Id = 3, ListId = 2 , Quantity = 3, StockId = 4 },
                new ListStock { Id = 4, ListId = 2 , Quantity = 3, StockId = 4 },
                new ListStock { Id = 5, ListId = 2 , Quantity = 3, StockId = 4 }
            });

            const int Id = 3;

            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{Id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.DoesNotContain(GetFromDatabase<ListStock>(), x => x.Id == Id);
        }

        [Fact]
        public async Task DeleteListStockAsync_StockNotFound_Fail()
        {
            await Authenticate();

            await AddToDatabase(new List<ListStock>
            {
                new ListStock { Id = 1, ListId = 2 , Quantity = 3, StockId = 4 },
                new ListStock { Id = 2, ListId = 2 , Quantity = 3, StockId = 4 },
                new ListStock { Id = 3, ListId = 2 , Quantity = 3, StockId = 4 },
                new ListStock { Id = 4, ListId = 2 , Quantity = 3, StockId = 4 },
                new ListStock { Id = 5, ListId = 2 , Quantity = 3, StockId = 4 }
            });

            const int Id = 2137;

            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{Id}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
