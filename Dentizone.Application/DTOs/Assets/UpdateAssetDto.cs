using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Assets;

public class UpdateAssetDto
{
    public AssetType Type { get; set; }
    public AssetStatus Status { get; set; }
}