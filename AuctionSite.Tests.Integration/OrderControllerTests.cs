using AuctionSite.Domain.Entity;
using AuctionSite.Models.Order;
using AuctionSite.Models.Order.Request;
using AuctionSite.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Integration
{
    public class OrderControllerTests : IntegrationTest
    {
        const string ApiUrl = "/api/order";

        private Order CreateTestOrder(int id, string userId = "id")
            => new Order
            {
                Address = $"addr {id}",
                UserId = userId,
                City = $"city {id}",
                CreationDate = DateTime.Now,
                Email = $"email{id}@email.com",
                Id = id,
                Name = $"name {id}",
                PaymentId = $"payment {id}",
                PhoneNumber = "1234567890",
                PostalCode = $"123{id}",
            };

        [Fact]
        public async Task PostOrderAsync_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            const int StockOnHoldId = 2;
            const int StockId = 4;
            const int StockQuantity = 3;

            var cart = new Cart
            {
                Id = 1,
                UserId = user.Id,
                StocksOnHold = new List<StockOnHold>
                {
                    new StockOnHold
                    {
                        Id = StockOnHoldId,
                        Quantity = StockQuantity,
                        Stock = new Stock
                        {
                            Id = StockId,
                            Product = new Product
                            {
                                Description = "desc",
                                Name = "name",
                                Price = 1234,
                                StockName = "stock",
                                OwnerId = "id"
                            },
                            Value = "val"
                        }
                    }
                }
            };

            await AddToDatabase(new List<Cart> { cart });

            var request = new AddOrderRequest
            {
                UserId = user.Id,
                Address = "addr",
                CartId = cart.Id,
                City = "krk",
                Email = "email@email.com",
                Name = "name",
                PaymentId = "payment",
                PhoneNumber = "1234567890",
                PostalCode = "12345"
            };

            var response = await _httpClient.PostAsJsonAsync(ApiUrl, request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains(GetFromDatabase<Order>(), x => x.UserId == user.Id);
            Assert.DoesNotContain(GetFromDatabase<StockOnHold>(), x => x.Id == StockOnHoldId);
            Assert.Contains(GetFromDatabase<OrderStock>(), x => x.StockId == StockId && x.Quantity == StockQuantity);
        }

        [Fact]
        public async Task GetOrderByIdAsync_Success()
        {
            await Authenticate();
            var user = GetAuthenticatedUser();

            var order = CreateTestOrder(2, user.Id);
            
            await AddToDatabase(new List<Order> { order });

            var response = await _httpClient.GetAsync($"{ApiUrl}/{order.Id}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<OrderModel>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(content.Success);
            Assert.Equal(order.Address, content.Data.Address);
            Assert.NotNull(content.Data.Items);
        }

        [Fact]
        public async Task GetOrderByIdAsync_NotFound()
        {
            await Authenticate();
            var user = GetAuthenticatedUser();

            var order = CreateTestOrder(2, user.Id);

            await AddToDatabase(new List<Order> { order });

            var response = await _httpClient.GetAsync($"{ApiUrl}/2137");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetOrdersByUserId_Success()
        {
            await Authenticate();
            var userId = GetAuthenticatedUser().Id;

            var orders = new List<Order>
            {
                CreateTestOrder(1, userId),
                CreateTestOrder(2),
                CreateTestOrder(3, userId),
                CreateTestOrder(4),
                CreateTestOrder(5, userId),
            };

            await AddToDatabase(orders);

            var response = await _httpClient.GetAsync($"{ApiUrl}/user/{userId}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<IEnumerable<OrderListItem>>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(content.Success);
            Assert.Equivalent(GetFromDatabase<Order>().Where(x => x.UserId == userId).Select(x => x.Id),
                content.Data.Select(x => x.Id));
        }
    }
}
