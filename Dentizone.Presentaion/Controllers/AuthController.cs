using Dentizone.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Dentizone.Application.Interfaces;
using Dentizone.Application.DTOs;


namespace Dentizone.Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController :ControllerBase
    {
        private readonly IAuthentication _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthController(IAuthentication authService, UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        private async Task<bool> IsUserLockedOutAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false; 

            return await _userManager.IsLockedOutAsync(user);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (result)
                return Ok("User registered successfully.");
            return BadRequest("User registration failed.");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (await IsUserLockedOutAsync(dto.Email))
                return Forbid("User account is locked out.");

            try
            {
                var token = await _authService.LoginAsync(dto.Email, dto.Password);
                return Ok(token);
            }
            catch (System.Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
       [ HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.LogoutAsync();
            if (result)
                return Ok("Logged out successfully.");
            return BadRequest("Logout failed.");
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] RestPasswordDTO dto)
        {
            var result = await _authService.ResetPasswordAsync(dto.Password, dto.NewPassword);
            if (result)
                return Ok("Password reset successfully.");
            return BadRequest("Password reset failed.");
        }




        }



}

