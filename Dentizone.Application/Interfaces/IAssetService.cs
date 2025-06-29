using Dentizone.Application.DTOs.Assets;

namespace Dentizone.Application.Interfaces;

public interface IAssetService
{
    Task<AssetDto> CreateAssetAsync(CreateAssetDto assetDto);
    Task<AssetDto> GetAssetByIdAsync(string id);
    Task<AssetDto> UpdateAssetAsync(string id, UpdateAssetDto assetDto);
    Task DeleteAssetAsync(string assetId);
}