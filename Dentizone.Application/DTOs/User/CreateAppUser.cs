using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.User
{
    public class CreateAppUser
    {
        public required string
            Id { get; set; }

        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string UniversityId { get; set; }
        public int AcademicYear { get; set; }

        public KycStatus KycStatus { get; set; }
        public UserState Status { get; set; }
    }
}