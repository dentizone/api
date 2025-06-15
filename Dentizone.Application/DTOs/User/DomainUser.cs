namespace Dentizone.Application.DTOs.User;

public class DomainUserView
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; }
    public int AcademicYear { get; set; }
    public long? NationalId { get; set; }
    public string KycStatus { get; set; }
    public string Status { get; set; }
    public string UnversityName { get; set; }
    public string Username { get; set; } = string.Empty;
}