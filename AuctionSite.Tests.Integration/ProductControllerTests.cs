﻿using AuctionSite.Domain.Entity;
using AuctionSite.Models.Product;
using AuctionSite.Models.Product.Request;
using AuctionSite.Models.Stock.Request;
using AuctionSite.Models.Response;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionSite.Tests.Integration
{
    public class ProductControllerTests : IntegrationTest
    {
        private const string ApiPath = "/api/product";

        private Product CreateTestProduct(int id, string userId)
            => new Product
            {
                Id = id,
                Description = $"desc {id}",
                Name = $"Name {id}",
                StockName = $"opt {id}",
                Stocks = new List<Stock>() { new Stock() { Value = $"value {id}", Quantity = 1, ProductId = id } },
                OwnerId = userId,
                Price = id * 10
            };

        [Fact]
        public async Task Get_NoParams_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();
            var products = new List<Product>
            {
                CreateTestProduct(1, user.Id),
                CreateTestProduct(2, user.Id),
                CreateTestProduct(3, user.Id),
                CreateTestProduct(4, user.Id),
                CreateTestProduct(5, user.Id),
                CreateTestProduct(6, user.Id),
                CreateTestProduct(7, user.Id),
                CreateTestProduct(8, user.Id),
                CreateTestProduct(9, user.Id),
                CreateTestProduct(10, user.Id),
                CreateTestProduct(11, user.Id),
            };

            await AddToDatabase(products);


            var response = await _httpClient.GetAsync(ApiPath);
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<IEnumerable<ProductListItemModel>>>();

            var testIds = GetFromDatabase<Product>().Take(10).Select(x => x.Id);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equivalent(testIds, content.Data.Select(x => x.Id));
        }

        [Fact]
        public async Task Get_PageSizeNotSpecified_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var products = new List<Product>
            {
                CreateTestProduct(1, user.Id),
                CreateTestProduct(2, user.Id),
                CreateTestProduct(3, user.Id),
                CreateTestProduct(4, user.Id),
                CreateTestProduct(5, user.Id),
                CreateTestProduct(6, user.Id),
                CreateTestProduct(7, user.Id),
                CreateTestProduct(8, user.Id),
                CreateTestProduct(9, user.Id),
                CreateTestProduct(10, user.Id),
                CreateTestProduct(11, user.Id),
            };

            await AddToDatabase(products);

            const int Index = 1;
            var response = await _httpClient.GetAsync($"{ApiPath}?pageIndex={Index}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<IEnumerable<ProductListItemModel>>>();

            var testIds = GetFromDatabase<Product>().Skip(Index * 10).Take(10).Select(x => x.Id);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equivalent(testIds, content.Data.Select(x => x.Id));
        }

        [Fact]
        public async Task Get_PageIndexNotSpecified_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var products = new List<Product>
            {
                CreateTestProduct(1, user.Id),
                CreateTestProduct(2, user.Id),
                CreateTestProduct(3, user.Id),
                CreateTestProduct(4, user.Id),
                CreateTestProduct(5, user.Id),
                CreateTestProduct(6, user.Id),
                CreateTestProduct(7, user.Id),
                CreateTestProduct(8, user.Id),
                CreateTestProduct(9, user.Id),
                CreateTestProduct(10, user.Id),
                CreateTestProduct(11, user.Id),
            };

            await AddToDatabase(products);

            const int Size = 5;
            var response = await _httpClient.GetAsync($"{ApiPath}?pageSize={Size}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<IEnumerable<ProductListItemModel>>>();

            var testIds = GetFromDatabase<Product>().Take(Size).Select(x => x.Id);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equivalent(testIds, content.Data.Select(x => x.Id));
        }

        [Fact]
        public async Task Get_ParamsSpecified_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var products = new List<Product>
            {
                CreateTestProduct(1, user.Id),
                CreateTestProduct(2, user.Id),
                CreateTestProduct(3, user.Id),
                CreateTestProduct(4, user.Id),
                CreateTestProduct(5, user.Id),
                CreateTestProduct(6, user.Id),
                CreateTestProduct(7, user.Id),
                CreateTestProduct(8, user.Id),
                CreateTestProduct(9, user.Id),
                CreateTestProduct(10, user.Id),
                CreateTestProduct(11, user.Id),
            };

            await AddToDatabase(products);

            const int Size = 5;
            const int Index = 2;
            var response = await _httpClient.GetAsync($"{ApiPath}?pageSize={Size}&pageIndex={Index}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<IEnumerable<ProductListItemModel>>>();

            var testIds = GetFromDatabase<Product>().Skip(Index * Size).Take(Size).Select(x => x.Id);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equivalent(testIds, content.Data.Select(x => x.Id));
        }

        [Fact]
        public async Task Get_ById_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var product = new Product
            {
                Id = 1,
                Description = "Test",
                Name = "name",
                StockName = "option",
                Stocks = new Stock[]
                {
                    new Stock { ProductId = 1, Value = "val", Quantity = 1 }
                },
                OwnerId = user.Id,
                Price = 123
            };

            await AddToDatabase(new List<Product> { product });

            var response = await _httpClient.GetAsync($"{ApiPath}/{product.Id}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<ProductModel>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(product.Name, content.Data.Name);
            Assert.True(content.Success);
            Assert.Single(content.Data.Stocks);
        }

        [Fact]
        public async Task Get_ById_NotFound_Fail()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var product = new Product
            {
                Id = 1,
                Description = "Test",
                Name = "name",
                StockName = "option",
                Stocks = new Stock[]
                {
                    new Stock { ProductId = 1, Value = "val", Quantity = 1 }
                },
                OwnerId = user.Id,
                Price = 123
            };

            await AddToDatabase(new List<Product> { product });

            var response = await _httpClient.GetAsync($"{ApiPath}/{2137}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<ProductModel>>();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.False(content.Success);
            Assert.Null(content.Data);
        }

        [Fact]
        public async Task Post_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var request = new AddProductRequest
            {
                Name = "name",
                Description = "Test",
                StockName = "option",
                Stocks = new List<AddStockRequest>
                {
                    new AddStockRequest { Quantity = 2, Value = "val" },
                    new AddStockRequest { Quantity = 2, Value = "val2" },
                },
                Price = 123,
                UserId = user.Id,
            };

            var response = await _httpClient.PostAsJsonAsync(ApiPath, request);
            var content = await response.Content.ReadFromJsonAsync<ResponseModel>();

            var testProduct = GetFromDatabase<Product>().First();
            var testStocks = GetFromDatabase<Stock>().Where(x => x.ProductId == testProduct.Id);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(request.Name, testProduct.Name);
            Assert.Equivalent(request.Stocks.Select(x => x.Value), testStocks.Select(x => x.Value));
            Assert.Equal(request.Stocks.Count(), testStocks.Count());
            Assert.NotNull(content.NewEntityId);
        }

        [Fact]
        public async Task Post_InvalidRequest_Fail()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var request = new AddProductRequest
            {
                StockName = "option",
                Price = 123,
                UserId = user.Id,
                Name = "name",
                Description = "desc",
                Stocks = new List<AddStockRequest> { }
            };

            var response = await _httpClient.PostAsJsonAsync(ApiPath, request);
            var error = await response.Content.ReadFromJsonAsync<ResponseModel>();
            var t = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.DoesNotContain(GetFromDatabase<Product>(), x => x.Name == request.Name);
            Assert.Contains(error.ValidationErrors.Keys, x => x == "Stocks");
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            await AddToDatabase(new List<Product>
            {
                CreateTestProduct(1, user.Id),
                CreateTestProduct(2, user.Id),
                CreateTestProduct(3, user.Id),
                CreateTestProduct(4, user.Id),
            });

            const int Id = 3;

            var response = await _httpClient.DeleteAsync($"{ApiPath}/{Id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.DoesNotContain(GetFromDatabase<Product>(), x => x.Id == Id);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_Fail()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            await AddToDatabase(new List<Product>
            {
                CreateTestProduct(1, user.Id),
                CreateTestProduct(2, user.Id),
                CreateTestProduct(3, user.Id),
                CreateTestProduct(4, user.Id),
            });

            const int Id = 1237;

            var response = await _httpClient.DeleteAsync($"{ApiPath}/{Id}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetPageCount_SizeNotSpecified()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            await AddToDatabase(new List<Product>
            {
                CreateTestProduct(1, user.Id),
                CreateTestProduct(2, user.Id),
                CreateTestProduct(3, user.Id),
                CreateTestProduct(4, user.Id),
                CreateTestProduct(5, user.Id),
                CreateTestProduct(6, user.Id),
                CreateTestProduct(7, user.Id),
                CreateTestProduct(8, user.Id),
                CreateTestProduct(9, user.Id),
                CreateTestProduct(10, user.Id),
                CreateTestProduct(11, user.Id),
                CreateTestProduct(12, user.Id),
                CreateTestProduct(13, user.Id),
                CreateTestProduct(14, user.Id),
                CreateTestProduct(15, user.Id),
                CreateTestProduct(16, user.Id),
            });

            var response = await _httpClient.GetAsync($"{ApiPath}/page-count");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<int>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(content.Success);
            Assert.Equal(2, content.Data);
        }

        [Fact]
        public async Task GetPageCount_SizeSpecified()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            await AddToDatabase(new List<Product>
            {
                CreateTestProduct(1, user.Id),
                CreateTestProduct(2, user.Id),
                CreateTestProduct(3, user.Id),
                CreateTestProduct(4, user.Id),
                CreateTestProduct(5, user.Id),
                CreateTestProduct(6, user.Id),
                CreateTestProduct(7, user.Id),
                CreateTestProduct(8, user.Id),
                CreateTestProduct(9, user.Id),
                CreateTestProduct(10, user.Id),
                CreateTestProduct(11, user.Id),
                CreateTestProduct(12, user.Id),
                CreateTestProduct(13, user.Id),
                CreateTestProduct(14, user.Id),
                CreateTestProduct(15, user.Id),
                CreateTestProduct(16, user.Id),
            });

            var response = await _httpClient.GetAsync($"{ApiPath}/page-count?pageSize=5");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<int>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(content.Success);
            Assert.Equal(4, content.Data);
        }
    }
}
