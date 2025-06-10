namespace Dentizone.Application.DTOs.User
{
    public class UserView
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public int AcademicYear { get; set; }
        public long? NationalId { get; set; }
        public string KycStatus { get; set; }
        public string Status { get; set; }
        public string UnversityName { get; set; }
    }
}