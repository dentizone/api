using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.User
{
    public class CreateAppUser
    {
        public string
            Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string UniversityId { get; set; }
        public int AcademicYear { get; set; }

        public KycStatus KycStatus { get; set; }
        public UserState Status { get; set; }
    }
}