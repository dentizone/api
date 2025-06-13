using Dentizone.Application.DTOs.Asset;
using Microsoft.AspNetCore.Http;

namespace Dentizone.Application.Interfaces
{
    public interface IUploadService
    {
        public Task<AssetDto> UploadImageAsync(IFormFile file, string userId);
        public Task<AssetDto> FindAssetById(string id);
    }
}