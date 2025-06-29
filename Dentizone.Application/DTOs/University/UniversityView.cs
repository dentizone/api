namespace Dentizone.Application.DTOs.University
{
    public class UniversityView
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public bool IsSupported { get; set; }
        public required string Domain { get; set; }
    }
}