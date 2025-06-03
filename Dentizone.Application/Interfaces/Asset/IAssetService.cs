using Dentizone.Application.DTOs.Asset;

namespace Dentizone.Application.Interfaces.Asset;

public interface IAssetService
{
    Task<AssetDto> CreateAssetAsync(CreateAssetDto assetDto);
    Task<AssetDto> GetAssetByIdAsync(string id);
    Task<AssetDto> UpdateAssetAsync(string id, UpdateAssetDto assetDto);
    Task<AssetDto> DeleteAssetAsync(string id);
}