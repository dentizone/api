using Dentizone.Application.DTOs.Assets;
using Microsoft.AspNetCore.Http;

namespace Dentizone.Application.Interfaces
{
    public interface IUploadService
    {
        public Task<AssetDto> UploadImageAsync(IFormFile file, string userId);
        public Task<AssetDto> FindAssetById(string id);

        public Task DeleteAssetById(string id, string userId);
    }
}