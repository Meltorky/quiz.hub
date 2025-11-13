using Microsoft.AspNetCore.Http;
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
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthenticatedUserDTO>> Login([FromBody] LoginDTO dto)
        {
            var result = await _authService.Login(dto);
            return Ok(result);
        }


        [HttpPost("register")]
        public async Task<ActionResult<AuthenticatedUserDTO>> register([FromBody] RegisterDTO dto)
        {
            var result = await _authService.Register(dto);
            return Ok(result);
        }
    }
}
