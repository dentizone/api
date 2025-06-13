using Dentizone.Application.DTOs.Asset;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Interfaces.Asset;
using Dentizone.Application.Interfaces.Cloudinary;
using Dentizone.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Dentizone.Application.Services
{
    public class UploadService(ICloudinaryService cloudinaryService, IAssetService assetService)
        : IUploadService
    {
        public async Task<AssetDto> FindAssetById(string id)
        {
            var uploadedImage = await assetService.GetAssetByIdAsync(id);
            return uploadedImage;
        }

        public async Task<AssetDto> UploadImageAsync(IFormFile file, string userId)
        {
            await using var stream = file.OpenReadStream();
            var url = cloudinaryService.Upload(stream, file.FileName);
            var image = await assetService.CreateAssetAsync(new CreateAssetDto
            {
                Size = file.Length,
                Url = url,
                Type = AssetType.Image,
                UserId = userId
            });

            return image;
        }
    }
}