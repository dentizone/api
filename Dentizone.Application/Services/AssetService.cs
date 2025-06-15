using AutoMapper;
using Dentizone.Application.DTOs.Assets;
using Dentizone.Application.Interfaces.Assets;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Application.Services
{
    public class AssetService(IAssetRepository assetRepository, IMapper mapper) : IAssetService
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
            var u = mapper.Map(assetDto, existingAsset);
            var updatedAsset = await assetRepository.UpdateAsync(u);
            return mapper.Map<AssetDto>(updatedAsset);
        }

        public async Task DeleteAssetAsync(AssetDto assetDto)
        {
            var asset = mapper.Map<Asset>(assetDto);

            await assetRepository.DeleteAsync(asset);
        }
    }
}