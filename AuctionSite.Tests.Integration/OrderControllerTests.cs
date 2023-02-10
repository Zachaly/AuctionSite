using AuctionSite.Domain.Entity;
using AuctionSite.Domain.Enum;
using AuctionSite.Models.Order;
using AuctionSite.Models.Order.Request;
using AuctionSite.Models.Response;
using System.Runtime.CompilerServices;

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

            await AddToDatabase(cart);

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
            
            await AddToDatabase(order);

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

            await AddToDatabase(order);

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

        [Fact]
        public async Task GetOrderStocksByProductId_Success()
        {
            await Authenticate();

            const int ProductId = 2;

            var stocks = new List<Stock>
            {
                new Stock
                {
                    Id = 1,
                    ProductId = 1,
                    Quantity = 10,
                    Value = "val"
                },
                new Stock
                {
                    Id = 2,
                    ProductId = ProductId,
                    Quantity = 10,
                    Value = "val1"
                },
                new Stock
                {
                    Id = 3,
                    ProductId = ProductId,
                    Quantity = 20,
                    Value = "val2"
                }
            };

            await AddToDatabase(stocks);

            var order = new Order
            {
                Id = 1,
                Address = "addr",
                City = "krk",
                CreationDate = DateTime.Now,
                Email = "email@email.com",
                Name = "Test",
                PhoneNumber = "1234567890",
                PostalCode = "1234567890",
                PaymentId = "paymnet",
                UserId = "id"
            };

            await AddToDatabase(order);

            var orderStocks = new List<OrderStock>
            {
                new OrderStock
                {
                    Id = 1,
                    OrderId = order.Id,
                    Quantity = 10,
                    StockId = 1,
                },
                new OrderStock
                {
                    Id = 2,
                    OrderId = order.Id,
                    Quantity = 20,
                    StockId = 2,
                },
                new OrderStock
                {
                    Id = 3,
                    OrderId = order.Id,
                    Quantity = 30,
                    StockId = 3,
                },
                new OrderStock
                {
                    Id = 4,
                    OrderId = order.Id,
                    Quantity = 40,
                    StockId = 3,
                },
            };

            await AddToDatabase(orderStocks);

            var response = await _httpClient.GetAsync($"{ApiUrl}/stocks/{ProductId}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<IEnumerable<OrderManagementItem>>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equivalent(stocks.Where(x => x.ProductId == ProductId).Select(x => x.Value), content.Data.Select(x => x.StockName));
            Assert.Equivalent(orderStocks.Where(x => x.StockId == 2 || x.StockId == 3).Select(x => x.Id), content.Data.Select(x => x.OrderStockId));
        }

        [Fact]
        public async Task GetOrderStockById_Success()
        {
            await Authenticate();

            var orderStock = new OrderStock
            {
                Id = 1,
                OrderId = 2,
                Quantity = 10,
                StockId = 3,
                RealizationStatus = RealizationStatus.Pending,
                Order = new Order
                {
                    Id = 2,
                    Address = "addr",
                    City = "krk",
                    CreationDate = DateTime.Now,
                    Email = "email@email.com",
                    Name = "Test",
                    PhoneNumber = "1234567890",
                    PostalCode = "1234567890",
                    PaymentId = "paymnet",
                    UserId = "id"
                },
                Stock = new Stock
                {
                    Id = 3,
                    ProductId = 1,
                    Quantity = 20,
                    Value = "val2"
                }
            };

            await AddToDatabase(orderStock);

            var response = await _httpClient.GetAsync($"{ApiUrl}/stock/{orderStock.Id}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<OrderProductModel>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(orderStock.Id, content.Data.Id);
            Assert.Equal(orderStock.Quantity, content.Data.Quantity);
            Assert.Equal(orderStock.Order.Name, content.Data.Name);
        }

        [Fact]
        public async Task GetOrderStockById_NotFound_Fail()
        {
            await Authenticate();

            var response = await _httpClient.GetAsync($"{ApiUrl}/stock/2137");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<OrderProductModel>>();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(content.Data);
        }

        [Fact]
        public async Task MoveRealizationStatus_Success()
        {
            await Authenticate();

            const int Id = 2;
            var orderStocks = new List<OrderStock>
            {
                new OrderStock { Id = 1, RealizationStatus = RealizationStatus.Pending },
                new OrderStock 
                { 
                    Id = Id,
                    RealizationStatus = RealizationStatus.Pending,
                    Stock = new Stock
                    {
                        Value = "val"
                    },
                    Order = new Order
                    {
                        Address = "addr",
                        City = "krk",
                        CreationDate = DateTime.Now,
                        Email = "email@email.com",
                        Name = "Test",
                        PhoneNumber = "1234567890",
                        PostalCode = "1234567890",
                        PaymentId = "paymnet",
                        UserId = "id"
                    }
                },
                new OrderStock { Id = 3, RealizationStatus = RealizationStatus.Pending },
            };

            await AddToDatabase(orderStocks);

            var request = new MoveRealizationStatusRequest { StockId = Id };

            var response = await _httpClient.PatchAsJsonAsync($"{ApiUrl}/move-realization-status", request);
            
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Contains(GetFromDatabase<OrderStock>(), x => x.Id == Id && x.RealizationStatus == RealizationStatus.Shipment);
        }

        [Fact]
        public async Task MoveRealizationStatus_StockNotFound_Fail()
        {
            await Authenticate();

            const int Id = 2;
            var orderStocks = new List<OrderStock>
            {
                new OrderStock { Id = 1, RealizationStatus = RealizationStatus.Pending },
                new OrderStock { Id = 3, RealizationStatus = RealizationStatus.Pending },
            };

            await AddToDatabase(orderStocks);

            var request = new MoveRealizationStatusRequest { StockId = Id };

            var response = await _httpClient.PatchAsJsonAsync($"{ApiUrl}/move-realization-status", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task MoveRealizationStatus_StockDelivered_Fail()
        {
            await Authenticate();

            const int Id = 2;
            var orderStocks = new List<OrderStock>
            {
                new OrderStock { Id = 1, RealizationStatus = RealizationStatus.Pending },
                new OrderStock
                {
                    Id = Id,
                    RealizationStatus = RealizationStatus.Delivered,
                    Stock = new Stock
                    {
                        Value = "val"
                    },
                    Order = new Order
                    {
                        Address = "addr",
                        City = "krk",
                        CreationDate = DateTime.Now,
                        Email = "email@email.com",
                        Name = "Test",
                        PhoneNumber = "1234567890",
                        PostalCode = "1234567890",
                        PaymentId = "paymnet",
                        UserId = "id"
                    }
                },
                new OrderStock { Id = 3, RealizationStatus = RealizationStatus.Pending },
            };

            await AddToDatabase(orderStocks);

            var request = new MoveRealizationStatusRequest { StockId = Id };

            var response = await _httpClient.PatchAsJsonAsync($"{ApiUrl}/move-realization-status", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains(GetFromDatabase<OrderStock>(), x => x.Id == Id && x.RealizationStatus == RealizationStatus.Delivered);
        }
    }
}
