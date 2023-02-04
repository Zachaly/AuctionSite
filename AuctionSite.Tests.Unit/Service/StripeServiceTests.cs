using AuctionSite.Application;
using AuctionSite.Application.Abstraction;
using AuctionSite.Models.Payment.Request;
using AuctionSite.Models.Response;
using Moq;
using Stripe;

namespace AuctionSite.Tests.Unit.Service
{
    public class StripeServiceTests
    {
        private readonly Mock<IResponseFactory> _responseFactory;
        private readonly Mock<ChargeService> _chargeService;
        private readonly Mock<TokenService> _tokenService;
        private readonly Mock<CustomerService> _customerService;
        private readonly Mock<IStripeFactory> _stripeFactory;
        private readonly StripeService _service;

        public StripeServiceTests()
        {
            _chargeService = new Mock<ChargeService>();
            _tokenService = new Mock<TokenService>();
            _customerService = new Mock<CustomerService>();
            _stripeFactory = new Mock<IStripeFactory>();
            _responseFactory = new Mock<IResponseFactory>();
            _responseFactory.Setup(x => x.CreateSuccess(It.IsAny<string>()))
                .Returns((string id) => new DataResponseModel<string> { Data = id, Success = true });

            _responseFactory.Setup(x => x.CreateFailure<string>(It.IsAny<string>()))
                .Returns((string msg) => new DataResponseModel<string> { Success = false, Error = msg, Data = null });

            _service = new StripeService(_chargeService.Object, _customerService.Object, _tokenService.Object,
                _stripeFactory.Object, _responseFactory.Object);
        }

        [Fact]
        public async Task AddCustomerAsync_Success()
        {
            _stripeFactory.Setup(x => x.CreateTokenOptions(It.IsAny<AddStripeCustomerRequest>()))
                .Returns((AddStripeCustomerRequest request) => new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = request.CardNumber
                    }
                });

            _stripeFactory.Setup(x => x.CreateCustomerOptions(It.IsAny<AddStripeCustomerRequest>(), It.IsAny<string>()))
                .Returns((AddStripeCustomerRequest request, string id) => new CustomerCreateOptions
                {
                    Email = request.Email,
                    Source = id
                });

            const string TokenId = "iddd";

            _tokenService.Setup(x => x.CreateAsync(It.IsAny<TokenCreateOptions>(), null, default))
                .ReturnsAsync((TokenCreateOptions options, RequestOptions _, CancellationToken _)
                    => new Token
                    {
                        Id = TokenId,
                    });

            const string UserId = "userid";

            _customerService.Setup(x => x.CreateAsync(It.IsAny<CustomerCreateOptions>(), null, default))
                .ReturnsAsync((CustomerCreateOptions options, RequestOptions _, CancellationToken _)
                    => new Customer
                    {
                        Id = UserId,
                        Email = options.Email,
                    });

            var request = new AddStripeCustomerRequest
            {
                CardNumber = "1234567890",
                Email = "email@email.com"
            };

            var res = await _service.AddStripeCustomer(request);

            Assert.True(res.Success);
            Assert.Equal(UserId, res.Data);
        }

        [Fact]
        public async Task AddCustomerAsync_ExceptionThrown_Fail()
        {
            var customers = new List<Customer>();
            var tokens = new List<Token>();

            const string Error = "err";

            _stripeFactory.Setup(x => x.CreateTokenOptions(It.IsAny<AddStripeCustomerRequest>()))
                .Callback(() => throw new Exception(Error));

            var request = new AddStripeCustomerRequest
            {
                CardNumber = "1234567890",
                Email = "email@email.com"
            };

            var res = await _service.AddStripeCustomer(request);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
        }

        [Fact]
        public async Task AddPaymentAsync_Success()
        {
            _stripeFactory.Setup(x => x.CreateChargeOptions(It.IsAny<AddPaymentRequest>()))
                .Returns((AddPaymentRequest request) => new ChargeCreateOptions { Amount = request.Amount });

            const string Id = "idd";

            _chargeService.Setup(x => x.CreateAsync(It.IsAny<ChargeCreateOptions>(), null, default))
                .ReturnsAsync((ChargeCreateOptions options, RequestOptions _, CancellationToken _)
                    => new Charge
                    {
                        Id = Id,
                        Amount = options.Amount.Value
                    });

            var request = new AddPaymentRequest { Amount = 1000 };

            var res = await _service.AddStripePayment(request);

            Assert.True(res.Success);
            Assert.Equal(Id, res.Data);
        }

        [Fact]
        public async Task AddPaymentAsync_ExceptionThrown_Fail()
        {
            _stripeFactory.Setup(x => x.CreateChargeOptions(It.IsAny<AddPaymentRequest>()))
                .Returns((AddPaymentRequest request) => new ChargeCreateOptions { Amount = request.Amount });

            const string Error = "err";

            _chargeService.Setup(x => x.CreateAsync(It.IsAny<ChargeCreateOptions>(), null, default))
                .Callback(() => throw new Exception(Error));

            var request = new AddPaymentRequest { Amount = 1000 };

            var res = await _service.AddStripePayment(request);

            Assert.False(res.Success);
            Assert.Equal(Error, res.Error);
            Assert.Null(res.Data);
        }
    }
}
