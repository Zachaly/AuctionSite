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

        public async Task<DataResponseModel<string>> AddStripeCustomer(AddStripeCustomerRequest request)
        {
            try
            {
                var tokenOptions = _stripeFactory.CreateTokenOptions(request);

                var token = await _tokenService.CreateAsync(tokenOptions);

                var customerOptions = _stripeFactory.CreateCustomerOptions(request, token.Id);

                var customer = await _customerService.CreateAsync(customerOptions);

                return _responseFactory.CreateSuccess(customer.Id);
            }
            catch (Exception ex)
            {
                return _responseFactory.CreateFailure<string>(ex.Message);
            }
        }

        public async Task<DataResponseModel<string>> AddStripePayment(AddPaymentRequest request)
        {
            try
            {
                var chargeOptions = _stripeFactory.CreateChargeOptions(request);

                var charge = await _chargeService.CreateAsync(chargeOptions);

                return _responseFactory.CreateSuccess(charge.Id);
            }
            catch(Exception ex)
            {
                return _responseFactory.CreateFailure<string>(ex.Message);
            }
        }
    }
}
