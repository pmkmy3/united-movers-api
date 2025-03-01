using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using united_movers_api.Models;
using united_movers_api.Services;

namespace united_movers_api.Controllers
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

        // POST: api/Auth/Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Error checks
            if (String.IsNullOrEmpty(request.UserName))
            {
                return null;
            }
            else if (String.IsNullOrEmpty(request.Password))
            {
                return null;
            }

            // Try login
            var response = await _authService.LoginAsync(request);

            // Return responses
            if (response != null)
            {
                return Ok(response);
            }

            return null;

        }

        // POST: api/Auth/ChangePassword
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var response = _authService.ChangePassword(request);
            var UserId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (response)
            {
                return Ok(new { HasPasswordChanged = true, Message = "Password changed successfully." });
            }
            else
            {
                return BadRequest(new { HasPasswordChanged = false, Message = "Password change unsuccessful." });
            }
        }

        // POST: api/Auth/ForgotPassword
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] ChangePasswordRequest request)
        {
            var response = _authService.ForgotPassword(request);
            if (response)
            {
                return Ok(new { HasPasswordChanged = true, Message = "Password reset was done successfully." });
            }
            else
            {
                return BadRequest(new { HasPasswordChanged = false, Message = "Password reset was unsuccessful." });
            }
        }

    }
}
