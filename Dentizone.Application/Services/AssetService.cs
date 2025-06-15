using AutoMapper;
using Dentizone.Application.DTOs.Assets;
using Dentizone.Application.Interfaces.Assets;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;

        public AssetService(IAssetRepository assetRepository, IMapper mapper)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
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

        public async Task DeleteAssetAsync(string assetId)
        {
            await _assetRepository.DeleteAsync(assetId);
        }
    }
}