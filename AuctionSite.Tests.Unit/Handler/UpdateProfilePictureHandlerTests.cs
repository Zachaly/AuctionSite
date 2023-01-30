using AuctionSite.Application.Abstraction;
using AuctionSite.Application.Command;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Unit.Handler
{
    public class UpdateProfilePictureHandlerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManager;
        private readonly Mock<IResponseFactory> _responseFactory;
        private readonly Mock<IFileService> _fileService;
        private readonly UpdateProfilePictureHandler _handler;

        public UpdateProfilePictureHandlerTests()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            _userManager.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            _userManager.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            _responseFactory = new Mock<IResponseFactory>();
            _fileService = new Mock<IFileService>();

            _handler = new UpdateProfilePictureHandler(_fileService.Object, _responseFactory.Object, _userManager.Object);
        }

        [Fact]
        public async Task Handle_Success()
        {
            var user = new ApplicationUser { Id = "id", ProfilePicture = "pic" };

            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _userManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()));

            _fileService.Setup(x => x.SaveProfilePicture(It.IsAny<IFormFile>()))
                .ReturnsAsync((IFormFile file) => file.Name);

            _fileService.Setup(x => x.RemoveProfilePicture(It.IsAny<string>()));

            _responseFactory.Setup(x => x.CreateSuccess())
                .Returns(new ResponseModel { Success = true });

            const string FileName = "fname";
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(x => x.FileName).Returns(FileName);

            var command = new UpdateProfilePictureCommand
            {
                File = fileMock.Object,
                UserId = user.Id
            };

            var res = await _handler.Handle(command, CancellationToken.None);

            Assert.True(res.Success);
            Assert.Equal(FileName, user.ProfilePicture);
        }

        [Fact]
        public async Task Handle_UserNotFound_Fail()
        {
            var user = new ApplicationUser { Id = "id", ProfilePicture = "pic" };

            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            _userManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()));

            _fileService.Setup(x => x.SaveProfilePicture(It.IsAny<IFormFile>()))
                .ReturnsAsync((IFormFile file) => file.Name);

            _fileService.Setup(x => x.RemoveProfilePicture(It.IsAny<string>()));

            _responseFactory.Setup(x => x.CreateFailure(It.IsAny<string>()))
                .Returns((string error) => new ResponseModel { Success = false, Error = error });

            const string FileName = "fname";
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(x => x.FileName).Returns(FileName);

            var command = new UpdateProfilePictureCommand
            {
                File = fileMock.Object,
                UserId = user.Id
            };

            var res = await _handler.Handle(command, CancellationToken.None);

            Assert.False(res.Success);
            Assert.NotEqual(FileName, user.ProfilePicture);
        }

        [Fact]
        public async Task Handle_ExceptionThrown_Fail()
        {
            var user = new ApplicationUser { Id = "id", ProfilePicture = "pic" };

            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _userManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()));

            _fileService.Setup(x => x.SaveProfilePicture(It.IsAny<IFormFile>()))
                .ReturnsAsync((IFormFile file) => file.Name);

            const string Error = "error";
            _fileService.Setup(x => x.RemoveProfilePicture(It.IsAny<string>()))
                .Callback(() => throw new Exception(Error));

            _responseFactory.Setup(x => x.CreateFailure(It.IsAny<string>()))
                .Returns((string error) => new ResponseModel { Success = false, Error = error });

            const string FileName = "fname";
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(x => x.FileName).Returns(FileName);

            var command = new UpdateProfilePictureCommand
            {
                File = fileMock.Object,
                UserId = user.Id
            };

            var res = await _handler.Handle(command, CancellationToken.None);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
            Assert.NotEqual(FileName, user.ProfilePicture);
        }

    }
}
