﻿using AuctionSite.Api.Infrastructure;
using AuctionSite.Application.Abstraction;
using AuctionSite.Models;
using AuctionSite.Models.Product;
using AuctionSite.Models.Product.Request;
using AuctionSite.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Controllers
{
    [Route("/api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Returns list of products
        /// </summary>
        /// <response code="200">List of product models</response>
        /// <response code="400">Something went wrong</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<DataResponseModel<IEnumerable<ProductListItemModel>>> Get([FromQuery] PagedRequest pagedRequest)
        {
            var res = _productService.GetProducts(pagedRequest);

            return res.ReturnOkOrBadRequest();
        }

        /// <summary>
        /// Returns product with given id
        /// </summary>
        /// <response code="200">Product model</response>
        /// <response code="404">Product not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<DataResponseModel<ProductModel>> GetById(int id)
        {
            var res = _productService.GetProductById(id);

            return res.ReturnOkOrNotFound();
        }

        /// <summary>
        /// Creates new product with data given in request
        /// </summary>
        /// <response code="201">Product created successfully</response>
        /// <response code="400">Invalid request</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ResponseModel>> PostAsync([FromBody] AddProductRequest request)
        {
            var res = await _productService.AddProductAsync(request);

            return res.ReturnCreatedOrBadRequest("");
        }

        /// <summary>
        /// Removes product with given id from database
        /// </summary>
        /// <response code="200">Product removed successfully</response>
        /// <response code="400">Invalid id</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ResponseModel>> DeleteAsync(int id)
        {
            var res = await _productService.DeleteProductByIdAsync(id);

            return res.ReturnNoContentOrBadRequest();
        }
    }
}