using AutoMapper;
using Dentizone.Application.DTOs.Cart;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper.Carts
{
    internal class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartItemDto, Cart>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
                .ForMember(dest => dest.Post, opt => opt.MapFrom(src => new Post
                                                                        {
                                                                            Title = src.Title,
                                                                            Price = src.Price,
                                                                            PostAssets = new List<PostAsset>
                                                                                {
                                                                                    new PostAsset
                                                                                    {
                                                                                        Asset = new Domain.Entity.Asset
                                                                                            {
                                                                                                Url = src.Url
                                                                                            }
                                                                                    }
                                                                                }
                                                                        }))
                .ReverseMap()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src =>
                                                                    src.Post.PostAssets.FirstOrDefault().Asset.Url))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Post.Title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Post.Price));

            CreateMap<AddToCartDto, Cart>().ReverseMap();
        }
    }
}