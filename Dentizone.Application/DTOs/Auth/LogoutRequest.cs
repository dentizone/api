using System.ComponentModel.DataAnnotations;

namespace Dentizone.Application.DTOs.Auth;

public class LogoutRequest
{
    [Required] public string RefreshToken { get; set; } = string.Empty;
}