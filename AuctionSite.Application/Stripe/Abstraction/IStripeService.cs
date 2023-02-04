using AuctionSite.Models.Payment.Request;
using AuctionSite.Models.Response;

namespace AuctionSite.Application.Abstraction
{
    public interface IStripeService
    {
        Task<DataResponseModel<string>> AddStripeCustomer(AddStripeCustomerRequest request);
        Task<DataResponseModel<string>> AddStripePayment(AddPaymentRequest request);
    }
}
