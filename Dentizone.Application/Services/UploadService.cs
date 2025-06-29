using Dentizone.Application.DTOs.Assets;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Interfaces.Assets;
using Dentizone.Application.Interfaces.Cloudinary;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Dentizone.Application.Services
{
    public class UploadService(
        ICloudinaryService cloudinaryService,
        IAssetService assetService,
        IHttpContextAccessor accessor)
        : BaseService(accessor), IUploadService
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

        public async Task DeleteAssetById(string id)
        {
            await AuthorizeAdminOrOwnerAsync(id);

            await assetService.DeleteAssetAsync(id);
        }

        protected override async Task<string> GetOwnerIdAsync(string resourceId)
        {
            var asset = await assetService.GetAssetByIdAsync(resourceId);
            if (asset == null)
            {
                throw new NotFoundException($"Asset with id {resourceId} not found");
            }

            return asset.UserId;
        }
    }
}