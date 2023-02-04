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
            => new ChargeCreateOptions
            {
                Amount = request.Amount,
                Customer = request.CustomerId,
                ReceiptEmail = request.Email,
                Currency = "usd"
            };

        public CustomerCreateOptions CreateCustomerOptions(AddStripeCustomerRequest request, string tokenId)
            => new CustomerCreateOptions
            {
                Email = request.Email,
                Name = $"{request.FirstName} {request.LastName}",
                Source = tokenId
            };

        public TokenCreateOptions CreateTokenOptions(AddStripeCustomerRequest request)
            => new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = request.CardNumber,
                    Cvc = request.Cvc,
                    ExpMonth = request.ExpirationMonth,
                    ExpYear = request.ExpirationYear,
                    Name = request.CardName
                }
            };
    }
}
