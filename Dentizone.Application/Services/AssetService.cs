using AutoMapper;
using Dentizone.Application.DTOs.Assets;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace Dentizone.Application.Services
{
    public class AssetService(IAssetRepository assetRepository, IMapper mapper, IHttpContextAccessor contextAccessor)
        : BaseService(contextAccessor), IAssetService
    {
        public async Task<AssetDto> CreateAssetAsync(CreateAssetDto assetDto)
        {
            var asset = mapper.Map<Asset>(assetDto);

            var createdAsset = await assetRepository.CreateAsync(asset);


            return mapper.Map<AssetDto>(createdAsset);
        }

        public async Task<AssetDto> GetAssetByIdAsync(string id)
        {
            var asset = await assetRepository.GetByIdAsync(id) ??
                        throw new NotFoundException($"Asset with id {id} not found");
            return mapper.Map<AssetDto>(asset);
        }

        public async Task<AssetDto> UpdateAssetAsync(string id, UpdateAssetDto assetDto)
        {
            var existingAsset = await assetRepository.GetByIdAsync(id) ??
                                throw new NotFoundException($"Asset with id {id} not found");

            await AuthorizeAdminOrOwnerAsync(existingAsset.Id);

            var u = mapper.Map(assetDto, existingAsset);
            var updatedAsset = await assetRepository.UpdateAsync(u);
            return mapper.Map<AssetDto>(updatedAsset);
        }

        public async Task DeleteAssetAsync(string assetId)
        {
            await AuthorizeAdminOrOwnerAsync(assetId);
            await assetRepository.DeleteByIdAsync(assetId);
        }

        protected override async Task<string> GetOwnerIdAsync(string resourceId)
        {
            var asset = await assetRepository.GetByIdAsync(resourceId);
            if (asset == null)
            {
                throw new NotFoundException($"Asset with id {resourceId} not found");
            }

            return asset.UserId;
        }
    }
}