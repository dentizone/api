using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Assets;

public class AssetDto
{
    public required string Id { get; set; }
    public required string Url { get; set; }
    public required long Size { get; set; }
    public required AssetType Type { get; set; }
    public required string UserId { get; set; }
}