using Dentizone.Domain.Enums;
using Dentizone.Infrastructure.Identity;

namespace Dentizone.Application.DTOs.User;

public class LoggedInUser
{
    public ApplicationUser User { get; set; }
    public UserRoles Role { get; set; }
}