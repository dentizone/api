using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Asset;

public class AssetDto
{
    public string Id { get; set; }
    public string Url { get; set; }
    public long Size { get; set; }
    public AssetType Type { get; set; }
}