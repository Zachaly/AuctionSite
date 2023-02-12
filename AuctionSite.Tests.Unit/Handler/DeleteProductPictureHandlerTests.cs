using AuctionSite.Application.Abstraction;
using AuctionSite.Application.Command;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Response;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSite.Tests.Unit.Handler
{
    public class DeleteProductPictureHandlerTests
    {
        private readonly Mock<IFileService> _fileService;
        private readonly Mock<IProductImageRepository> _productImageRepository;
        private readonly Mock<IResponseFactory> _responseFactory;
        private readonly DeleteProductPictureHandler _handler;

        public DeleteProductPictureHandlerTests()
        {
            _fileService = new Mock<IFileService>();
            _productImageRepository = new Mock<IProductImageRepository>(); 
            _responseFactory = new Mock<IResponseFactory>();

            _handler = new DeleteProductPictureHandler(_productImageRepository.Object, _fileService.Object, _responseFactory.Object);
        }

        [Fact]
        public async Task Handle_Success()
        {
            _fileService.Setup(x => x.RemoveProfilePicture(It.IsAny<string>()));

            _productImageRepository.Setup(x => x.GetProductImageById(It.IsAny<int>(), It.IsAny<Func<ProductImage, string>>()))
                .Returns(() => "image.jpg");

            _productImageRepository.Setup(x => x.DeleteProductImageByIdAsync(It.IsAny<int>()));

            _responseFactory.Setup(x => x.CreateSuccess())
                .Returns(new ResponseModel { Success = true });

            var res = await _handler.Handle(new DeleteProductPictureCommand { ImageId = 1 }, default);

            Assert.True(res.Success);
        }

        [Fact]
        public async Task Handle_ExceptionThrown_Fail()
        {
            const string Error = "error";

            _fileService.Setup(x => x.RemoveProfilePicture(It.IsAny<string>()))
                .Callback(() => throw new Exception(Error));

            _productImageRepository.Setup(x => x.GetProductImageById(It.IsAny<int>(), It.IsAny<Func<ProductImage, string>>()))
                .Returns(() => "image.jpg");

            _productImageRepository.Setup(x => x.DeleteProductImageByIdAsync(It.IsAny<int>()));

            _responseFactory.Setup(x => x.CreateFailure(It.IsAny<string>()))
                .Returns((string err) => new ResponseModel { Success = true, Error = err });

            var res = await _handler.Handle(new DeleteProductPictureCommand { ImageId = 1 }, default);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
        }

        [Fact]
        public async Task Handle_ImageNotFound_Fail()
        {
            _productImageRepository.Setup(x => x.GetProductImageById(It.IsAny<int>(), It.IsAny<Func<ProductImage, string>>()))
                .Returns(() => null);

            _responseFactory.Setup(x => x.CreateFailure(It.IsAny<string>()))
                .Returns((string err) => new ResponseModel { Success = true, Error = err });

            var res = await _handler.Handle(new DeleteProductPictureCommand { ImageId = 1 }, default);

            Assert.False(res.Success);
        }
    }
}
