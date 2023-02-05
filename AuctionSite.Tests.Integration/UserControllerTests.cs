using AuctionSite.Domain.Entity;
using AuctionSite.Models.Response;
using AuctionSite.Models.User;
using AuctionSite.Models.User.Request;

namespace AuctionSite.Tests.Integration
{
    public class UserControllerTests : IntegrationTest
    {
        private const string ApiUrl = "/api/user";

        [Fact]
        public async Task GetUserByIdAsync_Success()
        {
            var user = new ApplicationUser
            {
                Id = "id",
                UserName = "user",
                Email = "test@mail.com",
                Info = new UserInfo
                {
                    UserId = "id",
                    FirstName = "first",
                }
            };

            await AddToDatabase(user);

            var response = await _httpClient.GetAsync($"{ApiUrl}/{user.Id}");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<UserProfileModel>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(content.Success);
            Assert.Equal(user.Info.FirstName, content.Data.FirstName);
            Assert.Equal(user.Id, content.Data.Id);
        }

        [Fact]
        public async Task GetUserByIdAsync_InvalidId_NotFound()
        {
            var user = new ApplicationUser
            {
                Id = "id",
                UserName = "user",
                Email = "test@mail.com",
                Info = new UserInfo
                {
                    UserId = "id",
                    FirstName = "first",
                }
            };

            await AddToDatabase(user);

            var response = await _httpClient.GetAsync($"{ApiUrl}/aaaa");
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<UserProfileModel>>();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.False(content.Success);
            Assert.Null(content.Data);
        }

        [Fact]
        public async Task UpdateUser_FullRequest_Success()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var request = new UpdateUserRequest
            {
                UserName = "username",
                Address = "addr",
                City = "city",
                Country = "ctr",
                FirstName = "fname",
                Gender = Domain.Enum.Gender.Male,
                Id = user.Id,
                LastName = "lname",
                PhoneNumber = "1234567",
                PostalCode = "321321"
            };

            var response = await _httpClient.PutAsJsonAsync(ApiUrl, request);

            var testUser = GetFromDatabase<ApplicationUser>().FirstOrDefault(x => x.Id == user.Id);
            testUser.Info = GetFromDatabase<UserInfo>().FirstOrDefault(x => x.UserId == user.Id);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal(request.UserName, testUser.UserName);
            Assert.Equal(request.Address, testUser.Info.Address);
            Assert.Equal(request.City, testUser.Info.City);
            Assert.Equal(request.Country, testUser.Info.Country);
            Assert.Equal(request.FirstName, testUser.Info.FirstName);
            Assert.Equal(request.Gender, testUser.Info.Gender);
            Assert.Equal(request.Id, testUser.Id);
            Assert.Equal(request.LastName, testUser.Info.LastName);
            Assert.Equal(request.PhoneNumber, testUser.Info.PhoneNumber);
            Assert.Equal(request.PostalCode, testUser.Info.PostalCode);
        }

        [Fact]
        public async Task UpdateUser_InvalidUserId_Fail()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var request = new UpdateUserRequest
            {
                UserName = "username",
                Address = "addr",
                City = "city",
                Country = "ctr",
                FirstName = "fname",
                Gender = Domain.Enum.Gender.Male,
                Id = "iddddd",
                LastName = "lname",
                PhoneNumber = "1234567",
                PostalCode = "321321"
            };

            var response = await _httpClient.PutAsJsonAsync(ApiUrl, request);

            var testUser = GetFromDatabase<ApplicationUser>().FirstOrDefault(x => x.Id == user.Id);
            testUser.Info = GetFromDatabase<UserInfo>().FirstOrDefault(x => x.UserId == user.Id);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(user.UserName, testUser.UserName);
        }

        [Fact]
        public async Task UpdateUser_InvalidData_Fail()
        {
            await Authenticate();

            var user = GetAuthenticatedUser();

            var request = new UpdateUserRequest
            {
                UserName = "username",
                Address = "addr",
                City = "city",
                Country = "ctr",
                FirstName = "fname",
                Gender = Domain.Enum.Gender.Male,
                Id = user.Id,
                LastName = "lname",
                PhoneNumber = new string('a', 21),
                PostalCode = "321321"
            };

            var response = await _httpClient.PutAsJsonAsync("/api/user", request);
            var error = await response.Content.ReadFromJsonAsync<ResponseModel>();

            var testUser = GetFromDatabase<ApplicationUser>().FirstOrDefault(x => x.Id == user.Id);
            testUser.Info = GetFromDatabase<UserInfo>().FirstOrDefault(x => x.UserId == user.Id);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(user.PhoneNumber, testUser.PhoneNumber);
            Assert.Contains(error.ValidationErrors.Keys, x => x == "PhoneNumber");
        }
    }
}
