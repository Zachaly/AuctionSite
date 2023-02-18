using AuctionSite.Api.Infrastructure;
using AuctionSite.Application.Abstraction;
using AuctionSite.Models.Category;
using AuctionSite.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Controllers
{
    [Route("/api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public CategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        /// <summary>
        /// Returns list of existing product categories
        /// </summary>
        /// <response code="200">Product category list</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<DataResponseModel<IEnumerable<CategoryModel>>>> GetAsync()
        {
            var res = await _productCategoryService.GetCategoriesAsync();

            return res.ReturnOkOrBadRequest();
        }
    }
}
