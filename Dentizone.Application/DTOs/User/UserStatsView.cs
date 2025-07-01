namespace Dentizone.Application.DTOs.User;

public class UserStatsView
{
    public int TotalUsers { get; set; }
    public Dictionary<string, int> UsersPerStatus { get; set; }
}