using AutoMapper;
using Dentizone.Application.DTOs.PostDTO;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper.Posts
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostDto, Post>()
                .ReverseMap();
            CreateMap<UpdatePostDto, Post>().ReverseMap();
            CreateMap<PostAsset, PostAssetView>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AssetId))    // Map AssetId to Id
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Asset.Url)) // Map Asset.Url to Url
                .ReverseMap();
            CreateMap<Post, PostViewDto>()
                .ForMember(dest => dest.Assets, opt => opt.MapFrom(src => src.PostAssets))
                .ForMember(dest => dest.Seller, opt => opt.MapFrom(src => src.Seller))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SubCatgory, opt => opt.MapFrom(src => src.SubCategory.Name))
                .ReverseMap();
        }
    }
}