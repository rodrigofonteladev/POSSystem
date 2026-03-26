using Microsoft.AspNetCore.Mvc;
using POSSystem.Application.DTOs.Auth;
using POSSystem.Application.Interfaces;

namespace POSSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            if (!response.IsSuccess)
            {
                if (response.IsLockedOut) return BadRequest(new { message = response.Message });

                return Unauthorized(new { message = response.Message });
            }

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            var response = await _authService.RegisterAsync(dto);
            if (!response.IsSuccess) return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto dto)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknow";
            var result = await _authService.RefreshTokenAsync(dto, ipAddress);
            if (!result.IsSuccess) return Unauthorized(new { message = result.Message });

            return Ok(result);
        }
    }
}