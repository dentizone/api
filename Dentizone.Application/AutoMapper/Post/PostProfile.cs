using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs.Catalog;
using Dentizone.Application.DTOs.PostDTO;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper.Post
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostViewDTO, Domain.Entity.Post>().ReverseMap();
            CreateMap<CreatePostDTO, Domain.Entity.Post>().ReverseMap();
            CreateMap<UpdatePostDTO, Domain.Entity.Post>().ReverseMap();
        }
    }
}
