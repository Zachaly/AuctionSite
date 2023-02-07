using AuctionSite.Api.Infrastructure;
using AuctionSite.Application.Abstraction;
using AuctionSite.Models.ListStock.Request;
using AuctionSite.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Controllers
{
    [Route("/api/list-stock")]
    [Authorize]
    public class ListStockController : ControllerBase
    {
        private readonly IListStockService _listStockService;

        public ListStockController(IListStockService listStockService)
        {
            _listStockService = listStockService;
        }

        /// <summary>
        /// Adds new list stock with given data to database
        /// </summary>
        /// <response code="201">Stock added successfully</response>
        /// <response code="400">Invalid data</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> PostListStockAsync(AddListStockRequest request)
        {
            var res = await _listStockService.AddListStockAsync(request);

            return res.ReturnCreatedOrBadRequest("");
        }

        /// <summary>
        /// Removes list stock with given id from database
        /// </summary>
        /// <response code="204">Stock removed successfully</response>
        /// <response code="400">Invalid id</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> DeleteListStockByIdAsync(int id)
        {
            var res = await _listStockService.DeleteListStockByIdAsync(id);

            return res.ReturnNoContentOrBadRequest();
        }
    }
}
