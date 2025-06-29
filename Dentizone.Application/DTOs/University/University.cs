namespace Dentizone.Application.DTOs.University;

public class UniversityDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Domain { get; set; }
    public bool IsSupported { get; set; }
}