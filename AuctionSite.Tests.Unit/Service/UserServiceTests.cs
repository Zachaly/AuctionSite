using AuctionSite.Application.Abstraction;
using AuctionSite.Application.User;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Response;
using AuctionSite.Models.User;
using AuctionSite.Models.User.Request;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AuctionSite.Tests.Unit.Service
{
    public class UserServiceTests : ServiceTest
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManager;
        private readonly Mock<IUserFactory> _userFactory;
        private readonly Mock<IUserInfoRepository> _userInfoRepository;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            _userManager.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            _userManager.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            _userFactory = new Mock<IUserFactory>();
            _userInfoRepository = new Mock<IUserInfoRepository>();

            _userService = new UserService(_userManager.Object, _userInfoRepository.Object, _responseFactory.Object, _userFactory.Object);
        }

        [Fact]
        public async Task GetUserByIdAsync_Success()
        {
            var testUser = new ApplicationUser { Id = "id2", Info = new UserInfo { FirstName = "name2" } };
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "id", Info = new UserInfo { FirstName = "name" }},
                testUser,
                new ApplicationUser { Id = "id3", Info = new UserInfo { FirstName = "name3" }}
            };

            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) => users.FirstOrDefault(x => x.Id == id));

            _userFactory.Setup(x => x.CreateProfileModel(It.IsAny<ApplicationUser>()))
                .Returns((ApplicationUser user) => new UserProfileModel { Id = user.Id, FirstName = user.Info.FirstName });

            MockDataResponse<UserProfileModel>();

            _userInfoRepository.Setup(x => x.GetUserInfoByIdAsync(It.IsAny<string>(), It.IsAny<Func<UserInfo, UserInfo>>()))
                .ReturnsAsync(testUser.Info);

            var res = await _userService.GetUserByIdAsync(testUser.Id);

            Assert.True(res.Success);
            Assert.Equal(testUser.Info.FirstName, res.Data.FirstName);
            Assert.Equal(testUser.Id, res.Data.Id);
        }

        [Fact]
        public async Task GetUserByIdAsync_UserNotFound()
        {
            var testUser = new ApplicationUser { Id = "id2", Info = new UserInfo { FirstName = "name2" } };
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "id", Info = new UserInfo { FirstName = "name" }},
                testUser,
                new ApplicationUser { Id = "id3", Info = new UserInfo { FirstName = "name3" }}
            };

            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) => users.FirstOrDefault(x => x.Id == id));

            MockDataResponse<UserProfileModel>();

            var res = await _userService.GetUserByIdAsync("iddd");

            Assert.False(res.Success);
            Assert.Null(res.Data);
        }

        [Fact]
        public async Task GetUserByIdAsync_Error_Fail()
        {
            var testUser = new ApplicationUser { Id = "id2", Info = new UserInfo { FirstName = "name2" } };
            
            const string ErrorMessage = "error";

            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .Callback(() => throw new Exception(ErrorMessage));

            MockDataResponse<UserProfileModel>();

            var res = await _userService.GetUserByIdAsync("iddd");

            Assert.False(res.Success);
            Assert.Null(res.Data);
            Assert.Equal(ErrorMessage, res.Error);
        }

        [Fact]
        public async Task UpdateUserAsync_NullRequest_NoChange_Success()
        {
            var testUser = new ApplicationUser { Id = "id", Info = new UserInfo { FirstName = "name" } };

            _userManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(testUser);

            _userInfoRepository.Setup(x => x.GetUserInfoByIdAsync(It.IsAny<string>(), It.IsAny<Func<UserInfo, UserInfo>>()))
                .ReturnsAsync(testUser.Info);

            var request = new UpdateUserRequest
            {
                Id = testUser.Id,
            };

            var res = await _userService.UpdateUserAsync(request);

            Assert.True(res.Success);
            Assert.Equal("name", testUser.Info.FirstName);
        }

        [Fact]
        public async Task UpdateUserAsync_FullRequest_Change_Success()
        {
            var testUser = new ApplicationUser { Id = "id", Info = new UserInfo { FirstName = "name" } };

            _userManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(testUser);

            _userInfoRepository.Setup(x => x.GetUserInfoByIdAsync(It.IsAny<string>(), It.IsAny<Func<UserInfo, UserInfo>>()))
                .ReturnsAsync(testUser.Info);

            var request = new UpdateUserRequest
            {
                Id = testUser.Id,
                FirstName = "new name",
                Address = "addr",
                City = "city",
                Country = "ctr",
                Gender = Domain.Enum.Gender.Male,
                LastName = "lname",
                PhoneNumber = "num",
                PostalCode = "post",
                UserName = "username"
            };

            var res = await _userService.UpdateUserAsync(request);

            Assert.True(res.Success);
            Assert.Equal(request.FirstName, testUser.Info.FirstName);
            Assert.Equal(request.Country, testUser.Info.Country);
            Assert.Equal(request.UserName, testUser.UserName);
            Assert.Equal(request.LastName, testUser.Info.LastName);
            Assert.Equal(request.Address, testUser.Info.Address);
            Assert.Equal(request.City, testUser.Info.City);
            Assert.Equal(request.Gender, testUser.Info.Gender);
            Assert.Equal(request.PhoneNumber, testUser.Info.PhoneNumber);
            Assert.Equal(request.PostalCode, testUser.Info.PostalCode);
        }

        [Fact]
        public async Task UpdateUserAsync_ExceptionThrown_Fail()
        {
            var testUser = new ApplicationUser { Id = "id", Info = new UserInfo { FirstName = "name" } };

            const string ErrorMessage = "error";

            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(testUser);

            _userManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                .Callback(() => throw new Exception(ErrorMessage));

            _userInfoRepository.Setup(x => x.GetUserInfoByIdAsync(It.IsAny<string>(), It.IsAny<Func<UserInfo, UserInfo>>()))
                .ReturnsAsync(testUser.Info);

            var request = new UpdateUserRequest
            {
                Id = testUser.Id,
            };

            var res = await _userService.UpdateUserAsync(request);

            Assert.False(res.Success);
            Assert.Equal(ErrorMessage, res.Error);
        }

        [Fact]
        public async Task UpdateUserAsync_UserNotFound_Fail()
        {
            var testUser = new ApplicationUser { Id = "id", Info = new UserInfo { FirstName = "name" } };

            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) => null);

            _userManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            var request = new UpdateUserRequest
            {
                Id = testUser.Id,
            };

            var res = await _userService.UpdateUserAsync(request);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task UpdateUserAsync_IdentityResultFail_Fail()
        {
            var testUser = new ApplicationUser { Id = "id", Info = new UserInfo { FirstName = "name" } };

            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) => testUser);

            _userManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError[] { }));

            var request = new UpdateUserRequest
            {
                Id = testUser.Id,
            };

            var res = await _userService.UpdateUserAsync(request);

            Assert.False(res.Success);
        }
    }
}
