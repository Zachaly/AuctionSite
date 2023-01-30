using AuctionSite.Application.Abstraction;
using AuctionSite.Application.Command;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.Response;
using Microsoft.AspNetCore.Http;
using Moq;

namespace AuctionSite.Tests.Unit.Handler
{
    public class SaveProductImagesHandlerTests
    {
        private readonly Mock<IProductImageRepository> _productImageRepository;
        private readonly Mock<IProductFactory> _productFactory;
        private readonly Mock<IResponseFactory> _responseFactory;
        private readonly Mock<IFileService> _fileService;
        private readonly SaveProductImagesHandler _handler;

        public SaveProductImagesHandlerTests()
        {
            _productImageRepository = new Mock<IProductImageRepository>();
            _productFactory = new Mock<IProductFactory>();
            _responseFactory = new Mock<IResponseFactory>();
            _fileService = new Mock<IFileService>();

            _handler = new SaveProductImagesHandler(_productImageRepository.Object, _responseFactory.Object, _productFactory.Object, _fileService.Object);
        }

        private IFormFile CreateFormFileMock(string name)
        {
            var mock = new Mock<IFormFile>();
            mock.Setup(x => x.Name).Returns(name);
            return mock.Object;
        }

        [Fact]
        public async Task Handle_Success()
        {
            var images = new List<ProductImage>();

            var command = new SaveProductImagesCommand
            {
                Images = new List<IFormFile>
                {
                    CreateFormFileMock("name1"),
                    CreateFormFileMock("name2"),
                    CreateFormFileMock("name3"),
                    CreateFormFileMock("name4")
                },
                ProductId = 1,
            };

            _productImageRepository.Setup(x => x.AddProductImagesAsync(It.IsAny<IEnumerable<ProductImage>>()))
                .Callback((IEnumerable<ProductImage> list) => images.AddRange(list));

            _productFactory.Setup(x => x.CreateImage(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((int id, string name) => new ProductImage { ProductId = id, FileName = name });

            _fileService.Setup(x => x.SaveProductImages(It.IsAny<IEnumerable<IFormFile>>()))
                .ReturnsAsync((IEnumerable<IFormFile> files) => files.Select(x => x.Name));

            _responseFactory.Setup(x=> x.CreateSuccess())
                .Returns(new ResponseModel { Success = true });

            var res = await _handler.Handle(command, CancellationToken.None);

            Assert.True(res.Success);
            Assert.Equal(images.Count, command.Images.Count());
            Assert.Equivalent(images.Select(x => x.FileName), command.Images.Select(x => x.Name));
        }

        [Fact]
        public async Task Handle_ExceptionThrown_Fail()
        {
            var images = new List<ProductImage>();

            var command = new SaveProductImagesCommand
            {
                Images = new List<IFormFile>
                {
                    CreateFormFileMock("name1"),
                    CreateFormFileMock("name2"),
                    CreateFormFileMock("name3"),
                    CreateFormFileMock("name4")
                },
                ProductId = 1,
            };

            const string Error = "error";

            _productImageRepository.Setup(x => x.AddProductImagesAsync(It.IsAny<IEnumerable<ProductImage>>()))
                .Callback((IEnumerable<ProductImage> list) => throw new Exception(Error));

            _productFactory.Setup(x => x.CreateImage(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((int id, string name) => new ProductImage { ProductId = id, FileName = name });

            _fileService.Setup(x => x.SaveProductImages(It.IsAny<IEnumerable<IFormFile>>()))
                .ReturnsAsync((IEnumerable<IFormFile> files) => files.Select(x => x.Name));

            _responseFactory.Setup(x => x.CreateFailure(It.IsAny<string>()))
                .Returns((string err) => new ResponseModel { Success = false, Error = err });

            var res = await _handler.Handle(command, CancellationToken.None);

            Assert.False(res.Success);
        }
    }
}
