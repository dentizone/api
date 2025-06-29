using Dentizone.Domain.Enums;
using Dentizone.Infrastructure.Identity;

namespace Dentizone.Application.DTOs.User;

public class LoggedInUser
{
    public required ApplicationUser User { get; set; }
    public UserRoles Role { get; set; }
}