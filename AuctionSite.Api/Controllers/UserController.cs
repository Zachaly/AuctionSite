using AuctionSite.Api.Infrastructure;
using AuctionSite.Application.Abstraction;
using AuctionSite.Application.Command;
using AuctionSite.Models.Response;
using AuctionSite.Models.User;
using AuctionSite.Models.User.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Controllers
{
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public UserController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }
        
        /// <summary>
        /// Returns user with given id
        /// </summary>
        /// <response code="200">User model</response>
        /// <response code="404">User not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<DataResponseModel<UserProfileModel>>> GetUserByIdAsync(string id)
        {
            var res = await _userService.GetUserByIdAsync(id);

            return res.ReturnOkOrNotFound();
        }

        /// <summary>
        /// Updates user with data given in request
        /// </summary>
        /// <response code="204">User updated successfully</response>
        /// <response code="400">Update failed</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ResponseModel>> UpdateUserAsync([FromBody] UpdateUserRequest request)
        {
            var res = await _userService.UpdateUserAsync(request);

            return res.ReturnNoContentOrBadRequest();
        }

        /// <summary>
        /// Updates profile picture of given user
        /// </summary>
        /// <response code="204">User updated successfully</response>
        /// <response code="400">Invalid request</response>
        [HttpPut("profile-picture")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult<ResponseModel>> UpdateProfilePictureAsync([FromForm] UpdateProfilePictureCommand command)
        {
            var res = await _mediator.Send(command);

            return res.ReturnNoContentOrBadRequest();
        }
    }
}
