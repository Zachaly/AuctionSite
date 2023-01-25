using AuctionSite.Domain.Entity;
using AuctionSite.Models.Product;
using AuctionSite.Models.Product.Request;
using AuctionSite.Models.ProductOption.Request;
using AuctionSite.Models.Response;

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
                OptionName = $"opt {id}",
                Options = new List<ProductOption>() { new ProductOption() { Value = $"value {id}", Quantity = 1, ProductId = id } },
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
                OptionName = "option",
                Options = new ProductOption[]
                {
                    new ProductOption { ProductId = 1, Value = "val", Quantity = 1 }
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
            Assert.Single(content.Data.Options);
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
                OptionName = "option",
                Options = new ProductOption[]
                {
                    new ProductOption { ProductId = 1, Value = "val", Quantity = 1 }
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
                OptionName = "option",
                Options = new List<AddProductOptionRequest>
                {
                    new AddProductOptionRequest { Quantity = 2, Value = "val" },
                    new AddProductOptionRequest { Quantity = 2, Value = "val2" },
                },
                Price = 123,
                UserId = user.Id,
            };

            var response = await _httpClient.PostAsJsonAsync(ApiPath, request);

            var testProduct = GetFromDatabase<Product>().First();
            var testOptions = GetFromDatabase<ProductOption>().Where(x => x.ProductId == testProduct.Id);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(request.Name, testProduct.Name);
            Assert.Equivalent(request.Options.Select(x => x.Value), testOptions.Select(x => x.Value));
            Assert.Equal(request.Options.Count(), testOptions.Count());
        }

        [Fact]
        public async Task Post_InvalidRequest_Fail()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var request = new AddProductRequest
            {
                Name = "name"
            };

            var response = await _httpClient.PostAsJsonAsync(ApiPath, request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.DoesNotContain(GetFromDatabase<Product>(), x => x.Name == request.Name);
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
    }
}
