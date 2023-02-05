using AuctionSite.Application.Abstraction;
using AuctionSite.Models.Response;
using Moq;

namespace AuctionSite.Tests.Unit.Service
{
    public class ServiceTest
    {
        protected readonly Mock<IResponseFactory> _responseFactory;

        public ServiceTest()
        {
            _responseFactory = new Mock<IResponseFactory>();

            _responseFactory.Setup(x => x.CreateSuccess())
                .Returns(new ResponseModel { Success = true });

            _responseFactory.Setup(x => x.CreateFailure(It.IsAny<string>()))
                .Returns((string msg) => new ResponseModel { Success = false, Error = msg });
        }

        protected void MockDataResponse<T>()
        {
            _responseFactory.Setup(x => x.CreateSuccess(It.IsAny<T>()))
                .Returns((T data) => new DataResponseModel<T> { Data = data, Success = true });

            _responseFactory.Setup(x => x.CreateFailure<T>(It.IsAny<string>()))
                .Returns((string msg) => new DataResponseModel<T> { Success = false, Error = msg });
        }
    }
}
