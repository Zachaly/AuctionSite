using AuctionSite.Application;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.User.Request;

namespace AuctionSite.Tests.Unit.Factory
{
    public class UserFactoryTests
    {
        private readonly UserFactory _factory;

        public UserFactoryTests()
        {
            _factory = new UserFactory();
        }

        [Fact]
        public void CreateInfo()
        {
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

            var info = _factory.CreateInfo(request, UserId);

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
            var request = new RegisterRequest
            {
                Email = "email",
                Username = "username",
            };

            var user = _factory.Create(request);

            Assert.Equal(request.Email, user.Email);
            Assert.Equal(request.Username, user.UserName);
        }

        [Fact]
        public void CreateLoginResponse()
        {
            var user = new ApplicationUser
            {
                UserName = "username",
            };

            const string Token = "token";

            var response = _factory.CreateLoginResponse(user, Token);

            Assert.Equal(user.UserName, response.UserName);
            Assert.Equal(Token, response.AuthToken);
        }

        [Fact]
        public void CreateProfileModel()
        {
            var user = new ApplicationUser
            {
                UserName = "username",
                Id = "id",
                Info = new UserInfo
                {
                    UserId = "id",
                    Address = "addr",
                    City = "city",
                    Country = "ctn",
                    FirstName = "first",
                    Gender = Domain.Enum.Gender.Male,
                    LastName = "last",
                    PhoneNumber = "phone",
                    PostalCode = "12345",
                },
            };

            var model = _factory.CreateProfileModel(user);

            Assert.Equal(user.UserName, model.UserName);
            Assert.Equal(user.Id, model.Id);
            Assert.Equal(user.Info.FirstName, model.FirstName);
            Assert.Equal(user.Info.Address, model.Address);
            Assert.Equal(user.Info.Country, model.Country);
            Assert.Equal(user.Info.LastName, model.LastName);
            Assert.Equal(user.Info.PhoneNumber, model.PhoneNumber);
            Assert.Equal(user.Info.PostalCode, model.PostalCode);
            Assert.Equal(user.Info.Gender, model.Gender);
            Assert.Equal(user.Info.City, model.City);
        }
    }
}
