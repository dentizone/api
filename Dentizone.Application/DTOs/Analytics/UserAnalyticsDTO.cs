namespace Dentizone.Application.DTOs.Analytics
{
    public class UserAnalyticsDto
    {
        public int TotalUsers { get; set; }
        public int NewUsersLast7Days { get; set; }
        public int NewUsersLast30Days { get; set; }

        public Dictionary<string, int> UsersByUniversity { get; set; } = new Dictionary<string, int>();
    }
}