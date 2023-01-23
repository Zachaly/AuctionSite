using AuctionSite.Domain.Entity;
using AuctionSite.Models.Response;
using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Response;
using System.Net;
using System.Net.Http.Json;

namespace AuctionSite.Tests.Integration
{
    public class AuthControllerTests : IntegrationTest
    {
        [Fact]
        public async Task RegisterAsync_FullData_Success()
        {
            var request = new RegisterRequest
            {
                Address = "addr",
                City = "cit",
                Country = "pl",
                Email = "email@email.com",
                FirstName = "fname",
                Gender = Domain.Enum.Gender.Male,
                LastName = "lname",
                Password = "zaq1@WSX",
                PhoneNumber = "1234567890",
                PostalCode = "1234",
                Username = "username"
            };

            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains(GetFromDatabase<ApplicationUser>(), user =>
                user.UserName == request.Username &&
                user.Email == request.Email);
            Assert.Contains(GetFromDatabase<UserInfo>(), info => 
                info.Address == request.Address &&
                info.City == request.City &&
                info.Country == request.Country &&
                info.FirstName == request.FirstName && 
                info.Gender == request.Gender &&
                info.LastName == request.LastName &&
                info.PhoneNumber == request.PhoneNumber &&
                info.PostalCode == request.PostalCode);
        }

        [Fact]
        public async Task RegisterAsync_OnlyRequiredData_Success()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = "username"
            };

            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains(GetFromDatabase<ApplicationUser>(), user =>
                user.UserName == request.Username &&
                user.Email == request.Email);
            Assert.NotEmpty(GetFromDatabase<UserInfo>());
        }

        [Fact]
        public async Task RegisterAsync_InvalidRequest_Fail()
        {
            var request = new RegisterRequest
            {
            };

            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.DoesNotContain(GetFromDatabase<ApplicationUser>(), user =>
                user.UserName == request.Username &&
                user.Email == request.Email);
            Assert.Empty(GetFromDatabase<UserInfo>());
        }

        [Fact]
        public async Task RegisterAsync_UserExists_Success()
        {
            var request = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = "username"
            };

            await _httpClient.PostAsJsonAsync("/api/auth/register", request);
            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains(GetFromDatabase<ApplicationUser>(), user =>
                user.UserName == request.Username &&
                user.Email == request.Email);
            Assert.Single(GetFromDatabase<UserInfo>());
        }

        [Fact]
        public async Task Login_Success()
        {
            var registerRequest = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = "username"
            };

            await _httpClient.PostAsJsonAsync("/api/auth/register", registerRequest);

            var loginRequest = new LoginRequest
            {
                Email = registerRequest.Email,
                Password = registerRequest.Password,
            };

            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginRequest);
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<LoginResponse>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(content);
            Assert.True(content.Success);
            Assert.Equal(GetFromDatabase<ApplicationUser>().FirstOrDefault(x => x.Email == loginRequest.Email).Id, content.Data.UserId);
        }

        [Fact]
        public async Task Login_WrongCredentials_Fail()
        {
            var registerRequest = new RegisterRequest
            {
                Email = "email@email.com",
                Password = "zaq1@WSX",
                Username = "username"
            };

            await _httpClient.PostAsJsonAsync("/api/auth/register", registerRequest);

            var loginRequest = new LoginRequest
            {
                Email = "juan pablo",
                Password = "2137",
            };

            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginRequest);
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<LoginResponse>>();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Null(content.Data);
            Assert.False(content.Success);
        }
    }
}
