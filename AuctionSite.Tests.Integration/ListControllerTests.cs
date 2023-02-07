using AuctionSite.Domain.Entity;
using AuctionSite.Models.Response;
using AuctionSite.Models.SaveList;
using AuctionSite.Models.SaveList.Request;

namespace AuctionSite.Tests.Integration
{
    public class ListControllerTests : IntegrationTest
    {
        private const string ApiUrl = "/api/list";

        [Fact]
        public async Task GetListByIdAsync_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var stock = new ListStock
            {
                Id = 2,
                ListId = 1,
                Quantity = 2,
                Stock = new Stock
                {
                    Id = 3,
                    Product = new Product
                    {
                        Id = 4,
                        Name = "prod",
                        StockName = "stock",
                        Description = "descf",
                        OwnerId = "id",
                        Price = 123,
                    },
                    Quantity = 10,
                    Value = "val"
                }
            };

            var list = new SaveList
            {
                Id = 3,
                Name = "list",
                UserId = user.Id,
                Stocks = new List<ListStock>
                {
                    stock
                }
            };

            await AddToDatabase(new List<SaveList>
            {
                new SaveList { Id = 1, Name = "list1", UserId = "id", },
                list,
                new SaveList { Id = 2, Name = "list2", UserId = "id", },
            });

            var response = await _httpClient.GetAsync($"{ApiUrl}/{list.Id}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<ListModel>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(content.Success);
            Assert.Equal(list.Name, content.Data.Name);
            Assert.Contains(content.Data.Items, x => x.Id == stock.Id && x.Quantity == stock.Id);
        }

        [Fact]
        public async Task GetListByIdAsync_NotFound_Fail()
        {
            await Authenticate();

            var response = await _httpClient.GetAsync($"{ApiUrl}/0");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<ListModel>>();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(content.Data);
        }

        [Fact]
        public async Task PostListAsync_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var request = new AddListRequest { UserId = user.Id, Name = "list" };

            var response = await _httpClient.PostAsJsonAsync(ApiUrl, request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains(GetFromDatabase<SaveList>(), x => x.UserId == request.UserId && x.Name == request.Name);
        }

        [Fact]
        public async Task GetUserLists_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            await AddToDatabase(new List<SaveList>
            {
                new SaveList { Id = 1, UserId = user.Id,  Name = "list1" },
                new SaveList { Id = 2, UserId = "id",  Name = "list2" },
                new SaveList { Id = 3, UserId = user.Id,  Name = "list3" },
                new SaveList { Id = 4, UserId = "id",  Name = "list4" },
                new SaveList { Id = 5, UserId = user.Id,  Name = "list5" }
            });

            var response = await _httpClient.GetAsync($"{ApiUrl}/user/{user.Id}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<IEnumerable<ListListModel>>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(content.Success);
            Assert.Equivalent(GetFromDatabase<SaveList>().Where(x => x.UserId == user.Id).Select(x => x.Name),
                content.Data.Select(x => x.Name));
            Assert.Equivalent(GetFromDatabase<SaveList>().Where(x => x.UserId == user.Id).Select(x => x.Id),
                content.Data.Select(x => x.Id));
        }

        [Fact]
        public async Task DeleteListById_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            await AddToDatabase(new List<SaveList>
            {
                new SaveList { Id = 1, UserId = user.Id,  Name = "list1" },
                new SaveList { Id = 2, UserId = "id",  Name = "list2" },
                new SaveList { Id = 3, UserId = user.Id,  Name = "list3" },
                new SaveList { Id = 4, UserId = "id",  Name = "list4" },
                new SaveList { Id = 5, UserId = user.Id,  Name = "list5" }
            });

            const int Id = 3;

            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{Id}");
            
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.DoesNotContain(GetFromDatabase<SaveList>(), x => x.Id == Id);
        }

        [Fact]
        public async Task DeleteListById_ListNotFound_Fail()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            await AddToDatabase(new List<SaveList>
            {
                new SaveList { Id = 1, UserId = user.Id,  Name = "list1" },
                new SaveList { Id = 2, UserId = "id",  Name = "list2" },
                new SaveList { Id = 3, UserId = user.Id,  Name = "list3" },
                new SaveList { Id = 4, UserId = "id",  Name = "list4" },
                new SaveList { Id = 5, UserId = user.Id,  Name = "list5" }
            });

            const int Id = 2137;

            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{Id}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
