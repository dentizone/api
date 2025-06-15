using AutoMapper;
using Dentizone.Application.DTOs.Assets;

namespace Dentizone.Application.AutoMapper.Asset
{
    internal class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<CreateAssetDto, AssetDto>().ReverseMap();
            CreateMap<UpdateAssetDto, AssetDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Url, opt => opt.Ignore())
                .ForMember(dest => dest.Size, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AssetDto, Domain.Entity.Asset>().ReverseMap();
            CreateMap<Dentizone.Domain.Entity.Asset, CreateAssetDto>().ReverseMap();
        }
    }
}