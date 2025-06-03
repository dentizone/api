using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Asset;

public class UpdateAssetDto
{
    public AssetType Type { get; set; }
    public AssetStatus Status { get; set; }
}