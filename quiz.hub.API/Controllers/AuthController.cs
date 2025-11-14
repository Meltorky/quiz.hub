using Microsoft.AspNetCore.Mvc;
using quiz.hub.Application.DTOs.Auth;
using quiz.hub.Application.Interfaces.IServices.Authentication;

namespace quiz.hub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="dto">Login request data</param>
        /// <response code="200">Returns JWT token and expiration</response>
        /// <response code="400">Bad Request</response>
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticatedUserDTO>> Login([FromQuery] LoginDTO dto)
        {
            var result = await _authService.Login(dto);
            return Ok(result);
        }


        /// <summary>
        /// Register
        /// </summary>
        /// <param name="dto">Registration request data</param>
        /// <response code="200">User creation result with token</response>
        /// <response code="400">Bad Request</response>
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticatedUserDTO>> register([FromBody] RegisterDTO dto)
        {
            var result = await _authService.Register(dto);
            return Ok(result);
        }
    }
}
