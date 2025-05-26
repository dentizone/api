using Dentizone.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Dentizone.Domain
{
    internal class AppUser : IdentityUser
    {

        public string FullName { get; set; }
        public int AcademicYear { get; set; }
        public string UniversityId { get; set; }
        public long? NationalId { get; set; }
        public KycStatus KycStatus { get; set; }
        public UserState Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

