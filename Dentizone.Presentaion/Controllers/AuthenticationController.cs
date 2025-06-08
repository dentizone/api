using Dentizone.Application.DTOs.Auth;
using Dentizone.Application.DTOs.User;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Interfaces.User;
using Dentizone.Domain.Enums;
using Dentizone.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var loggedInUser =
                await authenticationService.LoginWithEmailAndPassword(loginPayload.Email, loginPayload.Password);


            var token = tokenService.GenerateAccessToken(loggedInUser.User.Id, loggedInUser.User.Email,
                                                         loggedInUser.role.ToString());
            return Ok(new { Token = token, loggedInUser });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerPayloadDto)
        {
            var LoggedInUser = await authenticationService.RegisterWithEmailAndPassword(registerPayloadDto);
            var userDataDto = new CreateAppUser
            {
                FullName = registerPayloadDto.FullName,
                AcademicYear = registerPayloadDto.AcademicYear,
                UniversityId = registerPayloadDto.UniversityId,
                KycStatus = KycStatus.PENDING,
                Username = registerPayloadDto.Username,
                Status = UserState.PendingVerification,
                Id = LoggedInUser.User.Id, // IdentityServer uses string IDs for users
            };
            var userData = await userService.CreateAsync(userDataDto);
            var token = tokenService.GenerateAccessToken(LoggedInUser.User.Id, registerPayloadDto.Email,
                                                         LoggedInUser.role.ToString());
            return Ok(new { Token = token, LoggedInUser, userData });
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