namespace Dentizone.Application.DTOs.Auth
{
    public class ResetPasswordDto
    {
        public required string NewPassword { get; set; }
        public required string Token { get; set; }
        public required string Email { get; set; }
    }
}