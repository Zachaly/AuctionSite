using AuctionSite.Api.Infrastructure;
using AuctionSite.Application.Abstraction;
using AuctionSite.Models.Cart;
using AuctionSite.Models.Cart.Request;
using AuctionSite.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Controllers
{
    [Route("api/cart")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<DataResponseModel<CartModel>> GetCartByUserId(string userId)
        {
            var res = _cartService.GetCartByUserId(userId);

            return res.ReturnOkOrNotFound();
        }

        [HttpGet("count/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<DataResponseModel<int>> GetCartItemsCountByUserId(string userId)
        {
            var res = _cartService.GetCartItemsCount(userId);

            return res.ReturnOkOrBadRequest();
        }

        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> AddCartAsync([FromBody] AddCartRequest request)
        {
            var res = await _cartService.AddCartAsync(request);

            return res.ReturnCreatedOrBadRequest("");
        }

        [HttpPost("item")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> AddToCartAsync([FromBody] AddToCartRequest request)
        {
            var res = await _cartService.AddToCartAsync(request);

            return res.ReturnCreatedOrBadRequest("");
        }

        [HttpDelete("item/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> DeleteCartItemAsync(int id)
        {
            var res = await _cartService.RemoveFromCartAsync(id);

            return res.ReturnNoContentOrBadRequest();
        }
    }
}
