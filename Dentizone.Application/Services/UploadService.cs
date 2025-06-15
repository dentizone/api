using Dentizone.Application.DTOs.Assets;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Interfaces.Assets;
using Dentizone.Application.Interfaces.Cloudinary;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
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

        public async Task<AssetDto> DeleteAssetById(string id, string userId)
        {
            var asset = await assetService.GetAssetByIdAsync(id);

            if (asset == null)
            {
                throw new NotFoundException("Asset not found");
            }

            if (asset.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this asset");
            }

            await assetService.DeleteAssetAsync(asset);
            return asset;
        }
    }
}