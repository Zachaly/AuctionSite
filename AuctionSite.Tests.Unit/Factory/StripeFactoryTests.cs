using AuctionSite.Application;
using AuctionSite.Models.Payment.Request;
using Stripe;

namespace AuctionSite.Tests.Unit.Factory
{
    public class StripeFactoryTests
    {
        private readonly StripeFactory _factory;

        public StripeFactoryTests()
        {
            _factory = new StripeFactory();
        }

        [Fact]
        public void CreateTokenOptions()
        {
            var request = new AddStripeCustomerRequest
            {
                CardName = "Jonh Smith",
                CardNumber = "213769694200",
                Cvc = "123",
                Email = "email@email.com",
                ExpirationMonth = "12",
                ExpirationYear = "2026",
                FirstName = "John",
                LastName = "Smith"
            };

            var options = _factory.CreateTokenOptions(request);

            var card = options.Card.Value as TokenCardOptions;

            Assert.Equal(request.CardName, card.Name);
            Assert.Equal(request.CardNumber, card.Number);
            Assert.Equal(request.Cvc, card.Cvc);
            Assert.Equal(request.ExpirationYear, card.ExpYear);
            Assert.Equal(request.ExpirationMonth, card.ExpMonth);
        }

        [Fact]
        public void CreateCustomerOptions()
        {
            var request = new AddStripeCustomerRequest
            {
                CardName = "Jonh Smith",
                CardNumber = "213769694200",
                Cvc = "123",
                Email = "email@email.com",
                ExpirationMonth = "12",
                ExpirationYear = "2026",
                FirstName = "John",
                LastName = "Smith"
            };

            const string TokenId = "idd";

            var options = _factory.CreateCustomerOptions(request, TokenId);

            Assert.Equal(request.FirstName.Concat(" ").Concat(request.LastName), options.Name);
            Assert.Equal(request.Email, options.Email);
            Assert.Equal(TokenId, options.Source.Value);
        }

        [Fact]
        public void CreateChargeOptions()
        {
            var request = new AddPaymentRequest
            {
                Amount = 1000,
                CustomerId = "123",
                Email = "email@email.com"
            };

            var options = _factory.CreateChargeOptions(request);

            Assert.Equal(request.Amount, options.Amount);
            Assert.Equal(request.CustomerId, options.Customer);
            Assert.Equal(request.Email, options.ReceiptEmail);
        }
    }
}
