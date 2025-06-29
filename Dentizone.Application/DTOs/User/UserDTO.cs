using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.User
{
    public class UserDto
    {
        public required string
            Id { get; set; } // YES, we're accepting string IDs for user because it's coming from IdentityServer

        public required string FullName { get; set; }
        public required string Username { get; set; }
        public int AcademicYear { get; set; }
        public long? NationalId { get; set; }
        public KycStatus KycStatus { get; set; }
        public UserState Status { get; set; }
        public required string UniversityName { get; set; }
    }
}