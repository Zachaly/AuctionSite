using AuctionSite.Models.Payment.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Tests.Integration
{
    public class StripeControllerTests : IntegrationTest
    {
        const string ApiUrl = "/api/stripe";

        [Fact]
        public async Task AddCustomerAsync_Success()
        {
            await Authenticate();

            var request = new AddStripeCustomerRequest
            {
                CardName = "John Kowalski",
                CardNumber = "4242 4242 4242 4242",
                Cvc = "123",
                Email = "email@email.com",
                ExpirationMonth = "5",
                ExpirationYear = "2025",
                FirstName = "John",
                LastName = "Kowalski"
            };

            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/customer", request);
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<string>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(content.Success);
            Assert.NotEmpty(content.Data);
        }

        [Fact]
        public async Task AddPaymentAsync_Success()
        {
            await Authenticate();

            var addCustomerRequest = new AddStripeCustomerRequest
            {
                CardName = "John Kowalski",
                CardNumber = "4242 4242 4242 4242",
                Cvc = "123",
                Email = "email@email.com",
                ExpirationMonth = "5",
                ExpirationYear = "2025",
                FirstName = "John",
                LastName = "Kowalski"
            };

            var customerResponse = await _httpClient.PostAsJsonAsync($"{ApiUrl}/customer", addCustomerRequest);
            var customerContent = await customerResponse.Content.ReadFromJsonAsync<DataResponseModel<string>>();

            var addPaymentRequest = new AddPaymentRequest
            {
                Amount = 100,
                CustomerId = customerContent.Data,
                Email = "email@email.com"
            };

            var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/payment", addPaymentRequest);
            var content = await response.Content.ReadFromJsonAsync<DataResponseModel<string>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(content.Success);
            Assert.NotEmpty(content.Data);
        }
    }
}
