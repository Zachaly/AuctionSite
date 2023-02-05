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
        
        /// <summary>
        /// Creates new stripe customer with data given in request
        /// </summary>
        /// <response code="200">Added user's id</response>
        /// <response code="400">Invalid request</response>
        [HttpPost("customer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<DataResponseModel<string>>> AddCustomerAsync(AddStripeCustomerRequest request)
        {
            var res = await _stripeService.AddStripeCustomer(request);

            return res.ReturnOkOrBadRequest();
        }

        /// <summary>
        /// Creates new stripe payment with data given in request
        /// </summary>
        /// <response code="200">New payment's id</response>
        /// <response code="400">Invalid request</response>
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
