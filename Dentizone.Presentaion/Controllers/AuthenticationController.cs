using System.Security.Claims;
using Dentizone.Application.DTOs.Auth;
using Dentizone.Application.DTOs.User;
using Dentizone.Application.Interfaces.User;
using Dentizone.Application.Services;
using Dentizone.Application.Services.Authentication;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController(
        IAuthService authenticationService,
        IUserService userService,
        ITokenService tokenService) : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginPayload)
        {
            var auth =
                await authenticationService.LoginWithEmailAndPassword(loginPayload.Email, loginPayload.Password);
            var token = tokenService.GenerateToken(auth.User.Id, auth.User.Email, UserRoles.GHOST.ToString());
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerPayloadDto)
        {
            var newUserId = await authenticationService.RegisterWithEmailAndPassword(registerPayloadDto);
            var userDataDto = new CreateAppUser
            {
                FullName = registerPayloadDto.FullName,
                AcademicYear = registerPayloadDto.AcademicYear,
                UniversityId = registerPayloadDto.UniversityId,
                KycStatus = KycStatus.PENDING,
                Username = registerPayloadDto.Username,
                Status = UserState.PendingVerification,
                Id = newUserId, // IdentityServer uses string IDs for users
            };
            var userData = await userService.CreateAsync(userDataDto);
            var token = tokenService.GenerateToken(newUserId, registerPayloadDto.Email, UserRoles.GHOST.ToString());
            return Ok(new { Token = token, userData });
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