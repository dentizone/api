namespace Dentizone.Presentaion.Controllers;

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; } = string.Empty;

    public string? AccessToken { get; set; }
}