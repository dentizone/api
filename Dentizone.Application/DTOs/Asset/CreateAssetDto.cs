using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Asset;

public class CreateAssetDto
{
    public string Url { get; set; }
    public long Size { get; set; }
    public AssetType Type { get; set; }
}