using Dentizone.Application.DTOs.Auth;
using Dentizone.Application.DTOs.User;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Mail;
using Dentizone.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Dentizone.Application.Services.Authentication
{
    public class AuthService(
        ITokenService tokenService,
        UserManager<ApplicationUser> userManager,
        IMailService mailService,
        IUserActivityService userActivityService
    )
        : IAuthService
    {
        private string GenerateToken(string userId, string email, string? role)
        {
            return tokenService.GenerateAccessToken(userId, email, role);
        }

        public async Task AlternateUserRoleAsync(UserRoles newRole, string userId)
        {
            var user = await userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            await AlternateUserRoleAsync(newRole, user);
        }

        public async Task AlternateUserRoleAsync(UserRoles newRole, ApplicationUser user)
        {
            var currentRoles = await userManager.GetRolesAsync(user);
            if (currentRoles.Any())
            {
                await userManager.RemoveFromRolesAsync(user, currentRoles);
            }

            await userManager.AddToRoleAsync(user, newRole.ToString());
        }

        public async Task<LoggedInUser> LoginWithEmailAndPassword(string email, string password)
        {
            // 1. Check if email exists

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new NotFoundException("Email or Password are not correct");
            }

            var isLockedOut = await userManager.IsLockedOutAsync(user);

            if (isLockedOut)
            {
                await userActivityService.CreateAsync(UserActivities.LOCKDOUT, DateTime.Now, user.Id);
                throw new
                    UserLockedOutException(
                                           "User is locked out due to too many failed login attempts. Please try again later.");
            }


            // 2. Check if password is correct
            var isPasswordValid = await userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid)
            {
                await userManager.AccessFailedAsync(user);
                throw new BadRequestException("Email or Password are not correct");
            }


            // 3. Check user role

            var roles = await userManager.GetRolesAsync(user);

            if (roles == null || !roles.Any())
            {
                throw new NotFoundException("User does not have any roles assigned");
            }

            // 4. Check if user is in role

            if (roles.Contains(UserRoles.BLACKLISTED.ToString()))
            {
                throw new BadRequestException("You're banned from using our platform.");
            }

            // 5. Generate token

            await userManager.ResetAccessFailedCountAsync(user);
            await userActivityService.CreateAsync(UserActivities.LOGIN, DateTime.Now, user.Id);
            return new LoggedInUser()
            {
                User = user,
                role = Enum.Parse<UserRoles>(roles.FirstOrDefault())
            };
        }

        public async Task<LoggedInUser> RegisterWithEmailAndPassword(RegisterRequestDto userData)
        {
            // 1. Check if email already exists
            var existingUser = await userManager.FindByEmailAsync(userData.Email);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException("User with this email already exists");
            }

            // 2. Create user
            var user = new ApplicationUser
            {
                UserName = userData.Username,
                Email = userData.Email,
            };
            var result = await userManager.CreateAsync(user, userData.Password);
            if (!result.Succeeded)
            {
                throw new BadRequestException("User creation failed: " +
                                              string.Join(", ", result.Errors.Select(e => e.Description)));
            }


            // 3. Assign default role
            await userManager.AddToRoleAsync(user, UserRoles.GHOST.ToString());

            // 4. Send Verification Email 

            await SendVerificationEmail(user.Email);
            return new LoggedInUser()
            {
                User = user,
                role = UserRoles.GHOST
            };
        }

        public async Task<string> ConfirmEmail(string token, string userId)
        {
            // 2. Get user from token
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            // check if already confirmed
            if (user.EmailConfirmed)
            {
                throw new BadRequestException("Email is already confirmed");
            }

            // 3. Confirm email
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new BadRequestException("Email confirmation failed: " +
                                              string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // 4. Assign verified role
            await AlternateUserRoleAsync(UserRoles.PARTILY_VERIFIED, user);
            await userActivityService.CreateAsync(UserActivities.EMAIL_CONFIRMED, DateTime.Now, user.Id);
            // 4. Generate token
            return GenerateToken(user.Id, user.Email, UserRoles.PARTILY_VERIFIED.ToString());
        }

        public async Task SendVerificationEmail(string email)
        {
            // 1. Check if email exists
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new NotFoundException("User with this email does not exist");
            }

            if (user.EmailConfirmed)
            {
                throw new BadRequestException("Your Email is already confined");
            }

            // 2. Generate token
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var verificationLink = $"https://dentizone.com/authverify-email?userId={user.Id}&token={token}";
            // 3. Send Verification Email
            await mailService.Send(email, "Dentizone: Verify your email",
                                   $"Please click the following link to verify your email: <a href=\"{verificationLink}\">Verify Email</a>");
        }

        public async Task SendForgetPasswordEmail(string email)
        {
            // 1. Check if email exists
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new NotFoundException("User with this email does not exist");
            }

            // 2. Generate token
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"https://dentizone.com/auth/reset-password?userId={user.Id}&token={token}";
            // 3. Send Reset Password Email
            await mailService.Send(email, "Dentizone: Reset your password",
                                   $"Please click the following link to reset your password: <a href=\"{resetLink}\">Reset Password</a>");
        }

        public async Task<ApplicationUser> GetById(string userId)
        {
            return await userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
        }

        public async Task<string> ResetPassword(string email, string token, string newPassword)
        {
            // 1. Check if email exists
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new NotFoundException("User with this email does not exist");
            }

            // 2. Reset password
            var result = await userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
            {
                throw new BadRequestException("Password reset failed: " +
                                              string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // Get user current role    
            var roles = await userManager.GetRolesAsync(user);
            if (roles == null || !roles.Any())
            {
                throw new NotFoundException("User does not have any roles assigned");
            }

            await userActivityService.CreateAsync(UserActivities.PASSWORD_RESET);
            // 3. Generate token
            return GenerateToken(user.Id, user.Email, roles.FirstOrDefault());
        }
    }
}