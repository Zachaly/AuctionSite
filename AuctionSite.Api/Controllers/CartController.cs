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

        /// <summary>
        /// Returns a cart of given user
        /// </summary>
        /// <response code="200">Cart Model</response>
        /// <response code="404">Cart not found</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<DataResponseModel<CartModel>> GetCartByUserId(string userId)
        {
            var res = _cartService.GetCartByUserId(userId);

            return res.ReturnOkOrNotFound();
        }

        /// <summary>
        /// Returns number of items in given user's cart
        /// </summary>
        /// <response code="200">Number of items</response>
        /// <response code="404">Cart does not exist</response>
        [HttpGet("count/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<DataResponseModel<int>> GetCartItemsCountByUserId(string userId)
        {
            var res = _cartService.GetCartItemsCount(userId);

            return res.ReturnOkOrBadRequest();
        }

        /// <summary>
        /// Adds new cart to database with data given in request
        /// </summary>
        /// <response code="201">Cart created successfully</response>
        /// <response code="400">Invald request</response>
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> AddCartAsync([FromBody] AddCartRequest request)
        {
            var res = await _cartService.AddCartAsync(request);

            return res.ReturnCreatedOrBadRequest("");
        }

        /// <summary>
        /// Adds new item to cart
        /// </summary>
        /// <response code="201">Cart item created successfully</response>
        /// <response code="400">Invalid request</response>
        [HttpPost("item")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> AddToCartAsync([FromBody] AddToCartRequest request)
        {
            var res = await _cartService.AddToCartAsync(request);

            return res.ReturnCreatedOrBadRequest("");
        }

        /// <summary>
        /// Removes item from cart
        /// </summary>
        /// <response code="204">Item removed successfully</response>
        /// <response code="400">Failed to remove item</response>
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
