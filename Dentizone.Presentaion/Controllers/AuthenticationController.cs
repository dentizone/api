using Dentizone.Application.DTOs.Auth;
using Dentizone.Application.DTOs.User;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;
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

            if (loggedInUser?.User.Email == null)
                throw new UnauthorizedAccessException("Invalid email or password");


            var token = tokenService.GenerateAccessToken(loggedInUser.User.Id, loggedInUser.User.Email,
                loggedInUser.Role.ToString());
            var refreshToken = tokenService.GenerateRefreshToken(loggedInUser.User.Id);
            return Ok(new RefreshTokenResponse()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
            });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerPayloadDto)
        {
            var loggedInUser = await authenticationService.RegisterWithEmailAndPassword(registerPayloadDto);
            var userDataDto = new CreateAppUser
            {
                FullName = registerPayloadDto.FullName,
                AcademicYear = registerPayloadDto.AcademicYear,
                UniversityId = registerPayloadDto.UniversityId,
                KycStatus = KycStatus.Pending,
                Username = registerPayloadDto.Username,
                Status = UserState.PendingVerification,
                Id = loggedInUser.User.Id, // IdentityServer uses string IDs for users
                Email = registerPayloadDto.Email
            };
            var userData = await userService.CreateAsync(userDataDto);

            var token = tokenService.GenerateAccessToken(loggedInUser.User.Id, registerPayloadDto.Email,
                loggedInUser.Role.ToString());
            var refreshToken = tokenService.GenerateRefreshToken(loggedInUser.User.Id);
            return Ok(new RefreshTokenResponse()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
            });
        }

        [HttpGet("confirm-email")]
        [Authorize]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;


            var result = await authenticationService.ConfirmEmail(token, userId);
            return Ok(new { Token = result });
        }

        [HttpGet("send-verification-email")]
        [Authorize]
        public async Task<IActionResult> SendVerificationEmail()
        {
            var email = User.Claims.First(c => c.Type == ClaimTypes.Email).Value;

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

        [HttpPost("refresh")]
        [AllowAnonymous] // Refresh doesn't require active authentication
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.RefreshToken))
                {
                    return BadRequest(new { message = "Refresh token is required" });
                }

                // Validate the refresh token
                var validationResult = await tokenService.ValidateRefreshTokenAsync(request.RefreshToken);

                if (!validationResult.IsValid)
                {
                    return Unauthorized(new { message = "Invalid or expired refresh token" });
                }

                // Extract user information from the refresh token
                var userId = validationResult.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "Invalid token claims" });
                }

                // This ensures that if user roles/permissions changed, the new token reflects that
                var user = await authenticationService.GetById(userId);
                var domainUser = await userService.GetByIdAsync(userId);
                if (user.Email == null)
                {
                    return Unauthorized(new { message = "User not found" });
                }

                switch (Enum.Parse<UserState>(domainUser.Status))
                {
                    case UserState.Active:
                    case UserState.PendingVerification:
                        break;
                    case UserState.Inactive:
                        return Unauthorized(new { message = "User account is inactive" });
                    case UserState.Suspended:
                        return Unauthorized(new { message = "User account is suspended" });
                    case UserState.Deleted:
                        return NotFound(new { message = "User account not found" });
                    case UserState.Banned:
                        return Unauthorized(new { message = "User account is banned" });
                    default:
                        return BadRequest(new { message = "Invalid user state" });
                }

                // Get Current User Role
                var userRole = await authenticationService.GetUserRoleAsync(userId);

                // Generate new tokens
                var newAccessToken = tokenService.GenerateAccessToken(user.Id, user.Email, userRole.ToString());
                var newRefreshToken = tokenService.GenerateRefreshToken(user.Id);

                // Blacklist the old refresh token to prevent reuse
                await tokenService.BlacklistRefreshTokenAsync(request.RefreshToken);


                if (!string.IsNullOrEmpty(request.AccessToken))
                {
                    await tokenService.BlacklistAccessTokenAsync(request.AccessToken);
                }


                return Ok(new RefreshTokenResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred during token refresh" });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest(new { message = "Invalid user context" });
                }


                // Get Access Token from header
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader) ||
                    !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest(new { message = "Missing or invalid authorization header" });
                }

                var token = authHeader.Substring("Bearer ".Length).Trim();
                await tokenService.BlacklistAccessTokenAsync(token);
                await tokenService.BlacklistRefreshTokenAsync(request.RefreshToken);


                return Ok(new { message = "Logged out successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred during logout" });
            }
        }
    }
}