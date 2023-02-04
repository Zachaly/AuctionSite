using AuctionSite.Api.Infrastructure;
using AuctionSite.Application.Abstraction;
using AuctionSite.Models.Payment.Request;
using AuctionSite.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Controllers
{
    [Route("/api/stripe")]
    [Authorize]
    public class StripeController : ControllerBase
    {
        private readonly IStripeService _stripeService;

        public StripeController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost("customer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<DataResponseModel<string>>> AddCustomerAsync(AddStripeCustomerRequest request)
        {
            var res = await _stripeService.AddStripeCustomer(request);

            return res.ReturnOkOrBadRequest();
        }

        [HttpPost("payment")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<DataResponseModel<string>>> AddPaymentAsync(AddPaymentRequest request)
        {
            var res = await _stripeService.AddStripePayment(request);

            return res.ReturnOkOrBadRequest();
        }
    }
}
