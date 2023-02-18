using AuctionSite.Domain.Entity;
using AuctionSite.Models.Category;
using AuctionSite.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Integration
{
    public class CategoryControllerTests : IntegrationTest
    {
        private const string ApiUrl = "/api/category";

        [Fact]
        public async Task Get_Success()
        {
            var categories = new List<ProductCategory>()
            {
                new ProductCategory { Id = 1, Name = "name" },
                new ProductCategory { Id = 2, Name = "name2" },
                new ProductCategory { Id = 3, Name = "name3" },
                new ProductCategory { Id = 4, Name = "name4" },
                new ProductCategory { Id = 5, Name = "name5" },
            };

            await AddToDatabase(categories);

            var response = await _httpClient.GetAsync(ApiUrl);
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<IEnumerable<CategoryModel>>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equivalent(categories.Select(x => x.Id), content.Data.Select(x => x.Id));
            Assert.Equivalent(categories.Select(x => x.Name), content.Data.Select(x => x.Name));
        }
    }
}
