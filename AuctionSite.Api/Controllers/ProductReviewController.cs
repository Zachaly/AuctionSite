using AuctionSite.Api.Infrastructure;
using AuctionSite.Application.Abstraction;
using AuctionSite.Models.ProductReview;
using AuctionSite.Models.ProductReview.Request;
using AuctionSite.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Controllers
{
    [Route("/api/product-review")]
    public class ProductReviewController : ControllerBase
    {
        private readonly IProductReviewService _productReviewService;

        public ProductReviewController(IProductReviewService productReviewService)
        {
            _productReviewService = productReviewService;
        }

        /// <summary>
        /// Returns reviews of given product
        /// </summary>
        /// <response code="200">List of reviews</response>
        [HttpGet("{productId}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<DataResponseModel<IEnumerable<ProductReviewListModel>>>> GetProductReviews(int productId)
        {
            var res = await _productReviewService.GetProductReviewsAsync(productId);

            return res.ReturnOkOrBadRequest();
        }

        /// <summary>
        /// Adds new review of given product
        /// </summary>
        /// <response code="201">Review created successfully</response>
        /// <response code="400">Invalid request</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ResponseModel>> PostReviewAsync(AddProductReviewRequest request)
        {
            var res = await _productReviewService.AddProductReviewAsync(request);

            return res.ReturnCreatedOrBadRequest("");
        }

        /// <summary>
        /// Updates given review with data given in request
        /// </summary>
        /// <response code="204">Review updated successfully</response>
        /// <response code="400">Failed to update review</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ResponseModel>> UpdateReviewAsync(UpdateProductReviewRequest request)
        {
            var res = await _productReviewService.UpdateProductReviewAsync(request);

            return res.ReturnNoContentOrBadRequest();
        }

        /// <summary>
        /// Deletes review with given id
        /// </summary>
        /// <response code="204">Review deleted successfully</response>
        /// <response code="400">Failed to remove review</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ResponseModel>> DeleteReviewByIdAsync(int id)
        {
            var res = await _productReviewService.DeleteProductReviewAsync(id);

            return res.ReturnNoContentOrBadRequest();
        }
    }
}
