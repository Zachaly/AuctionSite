using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Database.Repository.Abstraction;
using AuctionSite.Domain.Entity;
using AuctionSite.Models.ProductOption.Request;
using AuctionSite.Models.Response;
using Moq;

namespace AuctionSite.Tests.Unit.Service
{
    public class ProductOptionServiceTests
    {
        private readonly Mock<IResponseFactory> _responseFactory;
        private readonly Mock<IProductOptionFactory> _productOptionFactory;
        private readonly Mock<IProductOptionRepository> _productOptionRepository;
        private readonly ProductOptionService _service;

        public ProductOptionServiceTests()
        {
            _responseFactory = new Mock<IResponseFactory>();
            _productOptionFactory = new Mock<IProductOptionFactory>();
            _productOptionRepository = new Mock<IProductOptionRepository>();

            _service = new ProductOptionService(_responseFactory.Object, _productOptionFactory.Object, _productOptionRepository.Object);
        }

        [Fact]
        public async Task AddProductOptionAsync_Success()
        {
            var options = new List<ProductOption>();

            _productOptionRepository.Setup(x => x.AddProductOptionAsync(It.IsAny<ProductOption>()))
                .Callback((ProductOption option) => options.Add(option));

            _productOptionFactory.Setup(x => x.Create(It.IsAny<AddProductOptionRequest>()))
                .Returns((AddProductOptionRequest request) => new ProductOption
                {
                    ProductId = request.ProductId.GetValueOrDefault(),
                    Quantity = request.Quantity,
                    Value = request.Value
                });

            _responseFactory.Setup(x => x.CreateSuccess())
                .Returns(new ResponseModel { Success = true });

            var request = new AddProductOptionRequest
            {
                ProductId = 1,
                Quantity = 2,
                Value = "val"
            };

            var res = await _service.AddProductOptionAsync(request);

            Assert.Contains(options, x => x.Value == request.Value);
            Assert.True(res.Success);
        }

        [Fact]
        public async Task AddProductOptionAsync_ErrorThrown_Fail()
        {
            var options = new List<ProductOption>();

            const string ErrorMessage = "Error";

            _productOptionRepository.Setup(x => x.AddProductOptionAsync(It.IsAny<ProductOption>()))
                .Callback((ProductOption _) => throw new Exception(ErrorMessage));

            _productOptionFactory.Setup(x => x.Create(It.IsAny<AddProductOptionRequest>()))
                .Returns((AddProductOptionRequest request) => new ProductOption
                {
                    ProductId = request.ProductId.GetValueOrDefault(),
                    Quantity = request.Quantity,
                    Value = request.Value
                });

            _responseFactory.Setup(x => x.CreateFailure(It.IsAny<string>()))
                .Returns((string msg) => new ResponseModel { Success = false, Error = msg });

            var request = new AddProductOptionRequest
            {
                ProductId = 1,
                Quantity = 2,
                Value = "val"
            };

            var res = await _service.AddProductOptionAsync(request);

            Assert.False(res.Success);
            Assert.Equal(ErrorMessage, res.Error);
        }

        [Fact]
        public async Task DeleteProductOptionByIdAsync_Success()
        {
            var options = new List<ProductOption>
            {
                new ProductOption { Id = 1 },
                new ProductOption { Id = 2 },
                new ProductOption { Id = 3 },
                new ProductOption { Id = 4 },
            };

            _productOptionRepository.Setup(x => x.DeleteProductOptionByIdAsync(It.IsAny<int>()))
                .Callback((int id) => options.Remove(options.Find(x => x.Id == id)));

            _responseFactory.Setup(x => x.CreateSuccess())
                .Returns(new ResponseModel { Success = true });

            const int Id = 3;

            var res = await _service.DeleteProductOptionByIdAsync(Id);

            Assert.True(res.Success);
            Assert.DoesNotContain(options, x => x.Id == Id);
        }

        [Fact]
        public async Task DeleteProductOptionByIdAsync_ErrorThrown_Fail()
        {
            var options = new List<ProductOption>
            {
                new ProductOption { Id = 1 },
                new ProductOption { Id = 2 },
                new ProductOption { Id = 3 },
                new ProductOption { Id = 4 },
            };

            const string ErrorMessage = "Error";
            _productOptionRepository.Setup(x => x.DeleteProductOptionByIdAsync(It.IsAny<int>()))
                .Callback((int id) => throw new Exception(ErrorMessage));

            _responseFactory.Setup(x => x.CreateFailure(It.IsAny<string>()))
                .Returns((string msg) => new ResponseModel { Success = false, Error = msg });

            const int Id = 3;

            var res = await _service.DeleteProductOptionByIdAsync(Id);

            Assert.False(res.Success);
            Assert.Equal(ErrorMessage, res.Error);
        }
    }
}
