using AuctionSite.Models.Payment.Request;
using Stripe;

namespace AuctionSite.Application.Abstraction
{
    public interface IStripeFactory
    {
        TokenCreateOptions CreateTokenOptions(AddStripeCustomerRequest request);
        CustomerCreateOptions CreateCustomerOptions(AddStripeCustomerRequest request, string tokenId);
        ChargeCreateOptions CreateChargeOptions(AddPaymentRequest request);
    }
}
