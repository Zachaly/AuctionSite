using AuctionSite.Application.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Controllers
{
    [Route("/api/image")]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("profile/{id}")]
        public async Task<ActionResult> GetProfilePictureByIdAsync(string id)
        {
            var stream = await _mediator.Send(new GetProfilePictureQuery { UserId = id });

            return new FileStreamResult(stream, "image/png");
        }
    }
}
