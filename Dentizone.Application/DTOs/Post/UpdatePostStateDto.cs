using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Post;

public class UpdatePostStateDto
{
    public required string PostId { get; set; }
    public required PostStatus Status { get; set; }
    public string? Reason { get; set; }
}