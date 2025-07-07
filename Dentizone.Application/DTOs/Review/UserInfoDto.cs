namespace Dentizone.Application.DTOs.Review
{
    public class UserInfoDto
    {
        public required string Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public int AcademicYear { get; set; }
        public string UniversityId { get; set; } = string.Empty;
    }
}