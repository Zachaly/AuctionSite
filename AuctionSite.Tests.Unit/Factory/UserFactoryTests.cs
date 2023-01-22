using AuctionSite.Application;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.User.Request;

namespace AuctionSite.Tests.Unit.Factory
{
    public class UserFactoryTests
    {
        [Fact]
        public void CreateInfo()
        {
            var factory = new UserFactory();

            var request = new RegisterRequest
            {
                Address = "addr",
                City = "city",
                Country = "pl",
                FirstName = "fname",
                Gender = Domain.Enum.Gender.Male,
                LastName = "lname",
                PhoneNumber = "1234567890",
                PostalCode = "32-089",
            };

            const string UserId = "id";

            var info = factory.CreateInfo(request, UserId);

            Assert.Equal(UserId, info.UserId);
            Assert.Equal(request.Address, info.Address);
            Assert.Equal(request.City, info.City);
            Assert.Equal(request.Country, info.Country);
            Assert.Equal(request.FirstName, info.FirstName);
            Assert.Equal(request.Gender, info.Gender);
            Assert.Equal(request.LastName, info.LastName);
            Assert.Equal(request.PhoneNumber, info.PhoneNumber);
            Assert.Equal(request.PostalCode, info.PostalCode);
        }

        [Fact]
        public void Create()
        {
            var factory = new UserFactory();

            var request = new RegisterRequest
            {
                Email = "email",
                Username = "username",
            };

            var user = factory.Create(request);

            Assert.Equal(request.Email, user.Email);
            Assert.Equal(request.Username, user.UserName);
        }

        [Fact]
        public void CreateLoginResponse()
        {
            var factory = new UserFactory();

            var user = new ApplicationUser
            {
                UserName = "username",
            };

            const string Token = "token";

            var response = factory.CreateLoginResponse(user, Token);

            Assert.Equal(user.UserName, response.UserName);
            Assert.Equal(Token, response.AuthToken);
        }
    }
}
