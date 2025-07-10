using Dentizone.Application.DTOs.Auth;
using Dentizone.Application.DTOs.User;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
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
        ITokenService tokenService, IUserActivityService userActivityService) : ControllerBase
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
            await userActivityService.CreateAsync(UserActivities.Login, DateTime.UtcNow,
                loggedInUser.User.Id);
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
                Email = registerPayloadDto.Email,
                PhoneNumber = registerPayloadDto.PhoneNumber
            };
            await userService.CreateAsync(userDataDto);

            var token = tokenService.GenerateAccessToken(loggedInUser.User.Id, registerPayloadDto.Email,
                loggedInUser.Role.ToString());
            var refreshToken = tokenService.GenerateRefreshToken(loggedInUser.User.Id);
            await userActivityService.CreateAsync(UserActivities.Register, DateTime.UtcNow,
                loggedInUser.User.Id);
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

            await userActivityService.CreateAsync(UserActivities.EmailConfirmed, DateTime.UtcNow, userId);


            return Ok(new { Token = result });
        }

        [HttpGet("send-verification-email")]
        [Authorize]
        public async Task<IActionResult> SendVerificationEmail()
        {
            var email = User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            await authenticationService.SendVerificationEmail(email);
            await userActivityService.CreateAsync(UserActivities.EmailVerificationSent, DateTime.UtcNow, userId);

            return Ok();
        }


        [HttpPost("send-forget-password-email")]
        [AllowAnonymous]
        public async Task<IActionResult> SendForgetPasswordEmail([FromQuery] string email)
        {
            await authenticationService.SendForgetPasswordEmail(email);


            return Ok();
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
            // Validate the refresh token
            var validationResult = await tokenService.ValidateRefreshTokenAsync(request.RefreshToken);

            if (!validationResult.IsValid)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token");
            }

            // Extract user information from the refresh token
            var userId = validationResult.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            // This ensures that if user roles/permissions changed, the new token reflects that
            var user = await authenticationService.GetById(userId);
            var domainUser = await userService.GetByIdAsync(userId);
            if (user.Email == null)
            {
                throw new UnauthorizedAccessException("Invalid user ID or user not found");
            }

            switch (Enum.Parse<UserState>(domainUser.Status))
            {
                case UserState.PendingVerification:
                case UserState.EmailVerified:
                case UserState.Active:
                    break;
                case UserState.Blacklisted:
                    throw new UnauthorizedAccessException("You has been banned from our system");
                case UserState.Deleted:
                    throw new NotFoundException("User account not found");
                default:
                    throw new UnauthorizedAccessException("Invalid user state");
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

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
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

            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            await userActivityService.CreateAsync(UserActivities.Logout, DateTime.UtcNow, userId);


            return Ok();
        }
    }
}