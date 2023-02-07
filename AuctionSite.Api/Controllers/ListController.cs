using AuctionSite.Api.Infrastructure;
using AuctionSite.Application.Abstraction;
using AuctionSite.Models.Response;
using AuctionSite.Models.SaveList;
using AuctionSite.Models.SaveList.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Controllers
{
    [Route("/api/list")]
    [Authorize]
    public class ListController : ControllerBase
    {
        private readonly IListService _listService;

        public ListController(IListService listService)
        {
            _listService = listService;
        }

        /// <summary>
        /// Returns list with given id
        /// </summary>
        /// <response code="200">List model</response>
        /// <response code="404">Cannot find list with given id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<DataResponseModel<ListModel>> GetListById(int id)
        {
            var res = _listService.GetListById(id);

            return res.ReturnOkOrNotFound();
        }

        /// <summary>
        /// Adds new list with given data to database
        /// </summary>
        /// <response code="200">List added successfully</response>
        /// <response code="400">Invalid data</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> PostListAsync(AddListRequest request)
        {
            var res = await _listService.AddListAsync(request);

            return res.ReturnCreatedOrBadRequest("");
        }

        /// <summary>
        /// Returns lists of given user
        /// </summary>
        /// <response code="200">List of lists</response>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(200)]
        public ActionResult<DataResponseModel<IEnumerable<ListListModel>>> GetUserLists(string userId)
        {
            var res = _listService.GetUserLists(userId);

            return res.ReturnOkOrBadRequest();
        }

        /// <summary>
        /// Removes list with given id from database
        /// </summary>
        /// <response code="204">List removed successfully</response>
        /// <response code="400">Invalid id</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> DeleteListByIdAsync(int id) 
        { 
            var res = await _listService.DeleteListByIdAsync(id);

            return res.ReturnNoContentOrBadRequest();
        }
    }
}
