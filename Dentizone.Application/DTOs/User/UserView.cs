using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.User
{
    public class UserView
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public int AcademicYear { get; set; }
        public long? NationalId { get; set; }
        public KycStatus KycStatus { get; set; }
        public UserState Status { get; set; }
        public string UnversityName { get; set; }
    }
}