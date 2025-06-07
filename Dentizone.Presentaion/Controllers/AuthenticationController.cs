using Dentizone.Application.DTOs.Auth;
using Dentizone.Application.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController(IAuthService authenticationService) : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginPayload)
        {
            var auth =
                await authenticationService.LoginWithEmailAndPassword(loginPayload.Email, loginPayload.Password);

            return Ok(new { Token = auth });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerPayloadDto)
        {
            var auth = await authenticationService.RegisterWithEmailAndPassword(registerPayloadDto);
            return Ok(new { Token = auth });
        }

        [HttpGet("confirm-email")]
        [Authorize]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


            var result = await authenticationService.ConfirmEmail(token, userId);
            return Ok(new { Token = result });
        }

        [HttpGet("send-verification-email")]
        [Authorize]
        public async Task<IActionResult> SendVerificationEmail()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            await authenticationService.SendVerificationEmail(email);
            return Ok(new { Message = "Verification email sent successfully." });
        }


        [HttpPost("send-forget-password-email")]
        [AllowAnonymous]
        public async Task<IActionResult> SendForgetPasswordEmail([FromQuery] string email)
        {
            await authenticationService.SendForgetPasswordEmail(email);
            return Ok(new { Message = "Forget password email sent successfully." });
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var result = await authenticationService.ResetPassword(resetPasswordDto.Email, resetPasswordDto.Token,
                resetPasswordDto.NewPassword);


            return Ok(new { Message = result });
        }
    }
}