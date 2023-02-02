using AuctionSite.Application.Abstraction;
using AuctionSite.Domain.Util;
using AuctionSite.Models.Payment.Request;
using Stripe;

namespace AuctionSite.Application
{
    [Implementation(typeof(IStripeFactory))]
    public class StripeFactory : IStripeFactory
    {
        public ChargeCreateOptions CreateChargeOptions(AddPaymentRequest request)
        {
            throw new NotImplementedException();
        }

        public CustomerCreateOptions CreateCustomerOptions(AddStripeCustomerRequest request, string tokenId)
        {
            throw new NotImplementedException();
        }

        public TokenCreateOptions CreateTokenOptions(AddStripeCustomerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
