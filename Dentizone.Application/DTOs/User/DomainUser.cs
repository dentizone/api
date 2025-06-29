namespace Dentizone.Application.DTOs.User;

public class DomainUserView
{
    public string Id { get; set; } = string.Empty;
    public required string FullName { get; set; }
    public int AcademicYear { get; set; }
    public long? NationalId { get; set; }
    public required string KycStatus { get; set; }
    public required string Status { get; set; }
    public required string UnversityName { get; set; }
    public string Username { get; set; } = string.Empty;
}