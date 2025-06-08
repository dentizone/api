namespace Dentizone.Application.DTOs.Auth;

public class ConfirmEmailRequestDto
{
    public string Token { get; set; }
    public string UserId { get; set; }
}