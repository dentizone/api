namespace Dentizone.Application.DTOs.Auth
{
    public class ResetPasswordDto
    {
        public string NewPassword { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}