using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs.User;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
             CreateMap<UserDTO, AppUser>().ReverseMap();
            CreateMap<KycStatusDTO, AppUser>().ReverseMap();
            CreateMap<UserStateDTO, AppUser>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            CreateMap<CreatedUserDTO, AppUser>().ReverseMap();
        }
    }
}
