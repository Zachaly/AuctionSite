using AuctionSite.Api.Infrastructure;
using AuctionSite.Application.Abstraction;
using AuctionSite.Models.Response;
using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Response;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite.Api.Controllers
{
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> Register([FromBody] RegisterRequest request)
        {
            var res = await _authService.Register(request);

            return res.ReturnCreatedOrBadRequest("");
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<DataResponseModel<LoginResponse>>> Login([FromBody] LoginRequest request)
        {
            var res = await _authService.Login(request);

            return res.ReturnOkOrBadRequest();
        }
    }
}
