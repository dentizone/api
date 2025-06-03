using AutoMapper;
using Dentizone.Application.DTOs.Asset;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;

namespace Dentizone.Application.Services
{
    public class AssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;

        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }


        public async Task<AssetDto> CreateAssetAsync(CreateAssetDto assetDto)
        {
            var asset = _mapper.Map<Asset>(assetDto);

            var createdAsset = await _assetRepository.CreateAsync(asset);


            return _mapper.Map<AssetDto>(createdAsset);
        }

        public async Task<AssetDto> GetAssetByIdAsync(string id)
        {
            var asset = await _assetRepository.GetByIdAsync(id) ??
                        throw new NotFoundException($"Asset with id {id} not found");
            return _mapper.Map<AssetDto>(asset);
        }

        public async Task<AssetDto> UpdateAssetAsync(string id, UpdateAssetDto assetDto)
        {
            var existingAsset = await _assetRepository.GetByIdAsync(id) ??
                                throw new NotFoundException($"Asset with id {id} not found");
            var u = _mapper.Map(assetDto, existingAsset);
            var updatedAsset = await _assetRepository.UpdateAsync(u);
            return _mapper.Map<AssetDto>(updatedAsset);
        }

        public async Task<AssetDto> DeleteAssetAsync(string id)
        {
            var deletedAsset = await _assetRepository.DeleteAsync(id);
            return _mapper.Map<AssetDto>(deletedAsset);
        }

        public async Task<AssetDto> GetAssetByType(AssetType type)
        {
            var assets = await _assetRepository.FindBy(a => !a.IsDeleted && a.Type == type);
            return _mapper.Map<AssetDto>(assets);
        }
    }
}