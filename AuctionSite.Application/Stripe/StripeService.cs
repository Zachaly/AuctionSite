using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Payment.Request;
using AuctionSite.Models.Response;
using Stripe;

namespace AuctionSite.Application
{
    [Implementation(typeof(IStripeService))]
    public class StripeService : IStripeService
    {
        private readonly ChargeService _chargeService;
        private readonly CustomerService _customerService;
        private readonly TokenService _tokenService;
        private readonly IStripeFactory _stripeFactory;
        private readonly IResponseFactory _responseFactory;

        public StripeService(ChargeService chargeService, CustomerService customerService,
            TokenService tokenService, IStripeFactory stripeFactory,
            IResponseFactory responseFactory)
        {
            _chargeService = chargeService;
            _customerService = customerService;
            _tokenService = tokenService;
            _stripeFactory = stripeFactory;
            _responseFactory = responseFactory;
        }

        public Task<DataResponseModel<string>> AddStripeCustomer(AddStripeCustomerRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponseModel<string>> AddStripePayment(AddPaymentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
