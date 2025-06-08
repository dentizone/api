using Dentizone.Application.DTOs.Auth;
using Dentizone.Application.DTOs.User;
using Dentizone.Domain.Enums;
using Dentizone.Infrastructure.Identity;

namespace Dentizone.Application.Interfaces;

public interface IAuthService
{
    Task<LoggedInUser> LoginWithEmailAndPassword(string email, string password);
    Task<LoggedInUser> RegisterWithEmailAndPassword(RegisterRequestDto userData);
    Task<string> ConfirmEmail(string token, string userId);
    Task SendVerificationEmail(string email);
    Task<string> ResetPassword(string email, string token, string newPassword);
    Task SendForgetPasswordEmail(string email);
    Task<ApplicationUser> GetById(string userId);
    Task AlternateUserRoleAsync(UserRoles newRole, ApplicationUser user);
    Task AlternateUserRoleAsync(UserRoles newRole, string userId);
}