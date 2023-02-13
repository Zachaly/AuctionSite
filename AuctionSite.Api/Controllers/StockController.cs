using AuctionSite.Api.Infrastructure;
using AuctionSite.Application.Abstraction;
using AuctionSite.Models.Stock.Request;
using AuctionSite.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Controllers
{
    [Route("/api/stock")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        /// <summary>
        /// Creates new product stock with data given in request
        /// </summary>
        /// <response code="201">stock created successfully</response>
        /// <response code="400">Failed to add stock</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> PostAsync(AddStockRequest request)
        {
            var res = await _stockService.AddStockAsync(request);

            return res.ReturnCreatedOrBadRequest("");
        }

        /// <summary>
        /// Removes stock with given id from database
        /// </summary>
        /// <response code="204">stock removed successfully</response>
        /// <response code="400">Failed to remove stock</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> DeleteAsync(int id)
        {
            var res = await _stockService.DeleteStockByIdAsync(id);

            return res.ReturnNoContentOrBadRequest();
        }


        /// <summary>
        /// Updates stock with data given in request
        /// </summary>
        /// <response code="204">Stock updated successfully</response>
        /// <response code="400">Invalid request</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> UpdateStockAsync(UpdateStockRequest request)
        {
            var res = await _stockService.UpdateStockAsync(request);

            return res.ReturnNoContentOrBadRequest();
        }
    }
}
