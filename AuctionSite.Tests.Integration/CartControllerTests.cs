using AuctionSite.Domain.Entity;
using AuctionSite.Models.Cart;
using AuctionSite.Models.Cart.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Tests.Integration
{
    public class CartControllerTests : IntegrationTest
    {
        private const string ApiUrl = "/api/cart";

        [Fact]
        public async Task Put_NewCartAdded_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var request = new AddCartRequest
            {
                UserId = user.Id,
            };

            var response = await _httpClient.PutAsJsonAsync(ApiUrl, request);
            
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Single(GetFromDatabase<Cart>());
        }

        [Fact]
        public async Task Put_CartExists_NoCartAdded_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var cart = new Cart
            {
                Id = 1,
                UserId = user.Id,
            };

            await AddToDatabase(new List<Cart> { cart });

            var request = new AddCartRequest
            {
                UserId = user.Id,
            };

            var response = await _httpClient.PutAsJsonAsync(ApiUrl, request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Single(GetFromDatabase<Cart>());
            Assert.Contains(GetFromDatabase<Cart>(), x => x.Id == cart.Id);
        }

        [Fact]
        public async Task GetCartByUserId_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            const int CartId = 2;

            var stock = new Stock { Id = 1, ProductId = 1, Quantity = 2, Value = "val" };

            var product = new Product 
            { 
                Id = 1,
                Description = "desc",
                Name = "name",
                OwnerId = "id",
                StockName = "stock",
                Price = 123,
                Stocks = new List<Stock> { stock },
            };

            await AddToDatabase(new List<Product> { product });

            var carts = new List<Cart>
            {
                new Cart { Id = 1, UserId = "id" },
                new Cart 
                { 
                    Id = CartId,
                    UserId = user.Id,
                    StocksOnHold = new List<StockOnHold>
                    {
                        new StockOnHold { Quantity = 2, StockId = stock.Id },
                        new StockOnHold { Quantity = 3, StockId = stock.Id },
                        new StockOnHold { Quantity = 4, StockId = stock.Id },
                    }
                },
                new Cart { Id = 3, UserId = "idd" }
            };

            await AddToDatabase(carts);

            var response = await _httpClient.GetAsync($"{ApiUrl}/{user.Id}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<CartModel>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(content.Success);
            Assert.Equal(3, content.Data.Items.Count());
            Assert.Equal(CartId, content.Data.Id);
        }

        [Fact]
        public async Task GetCartByUserId_NotFound()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            const int CartId = 2;

            var stock = new Stock { Id = 1, ProductId = 1, Quantity = 2, Value = "val" };

            var product = new Product
            {
                Id = 1,
                Description = "desc",
                Name = "name",
                OwnerId = "id",
                StockName = "stock",
                Price = 123,
                Stocks = new List<Stock> { stock },
            };

            await AddToDatabase(new List<Product> { product });

            var carts = new List<Cart>
            {
                new Cart { Id = 1, UserId = "id" },
                new Cart
                {
                    Id = CartId,
                    UserId = user.Id,
                    StocksOnHold = new List<StockOnHold>
                    {
                        new StockOnHold { Quantity = 2, StockId = stock.Id },
                        new StockOnHold { Quantity = 3, StockId = stock.Id },
                        new StockOnHold { Quantity = 4, StockId = stock.Id },
                    }
                },
                new Cart { Id = 3, UserId = "idd" }
            };

            await AddToDatabase(carts);

            var response = await _httpClient.GetAsync($"{ApiUrl}/userid");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<CartModel>>();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.False(content.Success);
            Assert.Null(content.Data);
        }

        [Fact]
        public async Task GetCartItemsCountByUserId_CartExists_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var carts = new List<Cart>
            {
                new Cart { Id = 1, UserId = "id" },
                new Cart
                {
                    Id = 2,
                    UserId = user.Id,
                    StocksOnHold = new List<StockOnHold>
                    {
                        new StockOnHold { Quantity = 2, StockId = 1 },
                        new StockOnHold { Quantity = 3, StockId = 2 },
                        new StockOnHold { Quantity = 4, StockId = 3 },
                    }
                },
                new Cart { Id = 3, UserId = "idd" }
            };

            await AddToDatabase(carts);

            var response = await _httpClient.GetAsync($"{ApiUrl}/count/{user.Id}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<int>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(3, content.Data);
        }

        [Fact]
        public async Task GetCartItemsCountByUserId_CartDoesNotExists_ReturnsZero_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var response = await _httpClient.GetAsync($"{ApiUrl}/count/{user.Id}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<int>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(0, content.Data);
        }

        [Fact]
        public async Task AddToCart_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            const int StockId = 1;

            var product = new Product
            {
                Id = 1,
                Description = "desc",
                Name = "name",
                OwnerId = "id",
                Price = 123,
                StockName = "stock",
                Stocks = new List<Stock>
                {
                    new Stock
                    {
                        Id = StockId,
                        Quantity = 10,
                        Value = "val"
                    }
                }
            };

            await AddToDatabase(new List<Product> { product });

            var cart = new Cart { Id = 1, UserId = user.Id };

            await AddToDatabase(new List<Cart> { cart });

            var request = new AddToCartRequest
            {
                UserId = user.Id,
                Quantity = 5,
                StockId = StockId
            };

            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/item", request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(5, GetFromDatabase<Stock>().First(x => x.Id == 1).Quantity);
            Assert.Contains(GetFromDatabase<StockOnHold>(), x => x.Quantity == request.Quantity && x.StockId == StockId && x.CartId == cart.Id);
        }

        [Fact]
        public async Task AddToCart_QuantityExceedsStock_Fail()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            const int StockId = 1;

            var product = new Product
            {
                Id = 1,
                Description = "desc",
                Name = "name",
                OwnerId = "id",
                Price = 123,
                StockName = "stock",
                Stocks = new List<Stock>
                {
                    new Stock
                    {
                        Id = StockId,
                        Quantity = 10,
                        Value = "val"
                    }
                }
            };

            await AddToDatabase(new List<Product> { product });

            var cart = new Cart { Id = 1, UserId = user.Id };

            await AddToDatabase(new List<Cart> { cart });

            var request = new AddToCartRequest
            {
                UserId = user.Id,
                Quantity = 11,
                StockId = StockId
            };

            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/item", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(10, GetFromDatabase<Stock>().First(x => x.Id == 1).Quantity);
            Assert.Empty(GetFromDatabase<StockOnHold>());
        }

        [Fact]
        public async Task DeleteCartItemById_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            const int StockId = 3;

            var product = new Product
            {
                Id = 1,
                Description = "desc",
                Name = "name",
                OwnerId = "id",
                Price = 123,
                StockName = "stock",
                Stocks = new List<Stock>
                {
                    new Stock
                    {
                        Id = 1,
                        Quantity = 10,
                        Value = "val",
                        StocksOnHold = new List<StockOnHold>
                        {
                            new StockOnHold { Id = 1, Quantity = 5 },
                            new StockOnHold { Id = 2, Quantity = 4 },
                            new StockOnHold { Id = StockId, Quantity = 3 },
                            new StockOnHold { Id = 4, Quantity = 2 },
                        }
                    }
                }
            };

            await AddToDatabase(new List<Product> { product });

            var response = await _httpClient.DeleteAsync($"{ApiUrl}/item/{StockId}");
            
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal(13, GetFromDatabase<Stock>().First(x => x.Id == 1).Quantity);
            Assert.DoesNotContain(GetFromDatabase<StockOnHold>(), x => x.Id == StockId);
        }

        [Fact]
        public async Task DeleteCartItemById_NotFound_Fail()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var product = new Product
            {
                Id = 1,
                Description = "desc",
                Name = "name",
                OwnerId = "id",
                Price = 123,
                StockName = "stock",
                Stocks = new List<Stock>
                {
                    new Stock
                    {
                        Id = 1,
                        Quantity = 10,
                        Value = "val",
                        StocksOnHold = new List<StockOnHold>
                        {
                            new StockOnHold { Id = 1, Quantity = 5 },
                            new StockOnHold { Id = 2, Quantity = 4 },
                            new StockOnHold { Id = 3, Quantity = 3 },
                            new StockOnHold { Id = 4, Quantity = 2 },
                        }
                    }
                }
            };

            await AddToDatabase(new List<Product> { product });

            var response = await _httpClient.DeleteAsync($"{ApiUrl}/item/2137");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(10, GetFromDatabase<Stock>().First(x => x.Id == 1).Quantity);
        }
    }
}
