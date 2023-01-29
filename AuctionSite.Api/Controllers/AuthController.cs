using AuctionSite.Api.Infrastructure;
using AuctionSite.Application.Abstraction;
using AuctionSite.Models.Response;
using AuctionSite.Models.User.Request;
using AuctionSite.Models.User.Response;
using Microsoft.AspNetCore.Authorization;
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


        /// <summary>
        /// Creates new user
        /// </summary>
        /// <response code="201">User created successfully</response>
        /// <response code="400">User could not be created</response>
        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel>> Register([FromBody] RegisterRequest request)
        {
            var res = await _authService.Register(request);

            return res.ReturnCreatedOrBadRequest("");
        }

        /// <summary>
        /// Returns user authorization info
        /// </summary>
        /// <response code="200">Login model</response>
        /// <response code="400">Credentials are invalid</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<DataResponseModel<LoginResponse>>> Login([FromBody] LoginRequest request)
        {
            var res = await _authService.Login(request);

            return res.ReturnOkOrBadRequest();
        }

        /// <summary>
        /// Returns current user authorization info
        /// </summary>
        /// <response code="200">Login model</response>
        /// <response code="401">User is not authorized</response>
        [HttpGet("user")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<DataResponseModel<LoginResponse>>> GetUserDataAsync()
        {
            var res = await _authService.GetCurrentUserDataAsync();

            return res.ReturnOkOrBadRequest();
        }
    }
}
