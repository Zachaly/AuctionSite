using AuctionSite.Api.Infrastructure;
using AuctionSite.Application.Abstraction;
using AuctionSite.Models.Order;
using AuctionSite.Models.Order.Request;
using AuctionSite.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Controllers
{
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Adds order data to database
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201">Order addes successfully</response>
        /// <response code="400">Request is invalid</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> PostOrderAsync(AddOrderRequest request)
        {
            var res = await _orderService.AddOrderAsync(request);

            return res.ReturnCreatedOrBadRequest("");
        }

        /// <summary>
        /// Returns order with given id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Order model</response>
        /// <response code="404">Cannot find order with given id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<DataResponseModel<OrderModel>>> GetOrderByIdAsync(int id)
        {
            var res = await _orderService.GetOrderByIdAsync(id);

            return res.ReturnOkOrNotFound();
        }

        /// <summary>
        /// Returns list of orders of given user
        /// </summary>
        /// <param name="id"></param>
        /// <reponse code="200">List of orders</reponse>
        [HttpGet("user/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<DataResponseModel<IEnumerable<OrderListItem>>>> GetOrdersByUserIdAsync(string id)
        {
            var res = await _orderService.GetOrdersByUserIdAsync(id);

            return res.ReturnOkOrBadRequest();
        }
    }
}
