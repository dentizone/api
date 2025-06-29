namespace Dentizone.Application.DTOs.Auth;

public class ConfirmEmailRequestDto
{
    public required string Token { get; set; }
    public required string UserId { get; set; }
}