namespace Dentizone.Application.DTOs.User
{
    public class UserView
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int AcademicYear { get; set; }
        public string UniversityName { get; set; } = string.Empty;
    }
}