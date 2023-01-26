using AuctionSite.Api.Infrastructure;
using AuctionSite.Application.Abstraction;
using AuctionSite.Models.ProductOption.Request;
using AuctionSite.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Controllers
{
    [Route("/api/product-option")]
    public class ProductOptionController : ControllerBase
    {
        private readonly IProductOptionService _productOptionService;

        public ProductOptionController(IProductOptionService productOptionService)
        {
            _productOptionService = productOptionService;
        }

        /// <summary>
        /// Creates new product option with data given in request
        /// </summary>
        /// <response code="201">Option created successfully</response>
        /// <response code="400">Failed to add option</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> PostAsync(AddProductOptionRequest request)
        {
            var res = await _productOptionService.AddProductOptionAsync(request);

            return res.ReturnCreatedOrBadRequest("");
        }

        /// <summary>
        /// Removes option with given id from database
        /// </summary>
        /// <response code="204">Option removed successfully</response>
        /// <response code="400">Failed to remove option</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> DeleteAsync(int id)
        {
            var res = await _productOptionService.DeleteProductOptionByIdAsync(id);

            return res.ReturnNoContentOrBadRequest();
        }
    }
}
