using AutoMapper;
using Dentizone.Application.DTOs.PostDTO;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper.Posts
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostViewDto, Post>()
                .ReverseMap();
            CreateMap<CreatePostDto, Post>()
                .ReverseMap();
            CreateMap<UpdatePostDto, Post>().ReverseMap();
        }
    }
}