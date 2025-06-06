namespace Dentizone.Application.DTOs.Auth
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}