using AutoMapper;
using Dentizone.Application.DTOs.UserActivityDTO;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper
{
    public class UserActivityProfile : Profile
    {
        public UserActivityProfile()
        {
            CreateMap<CreatedUserActivityDto, UserActivity>().ReverseMap();
            CreateMap<UserActivityDTO, UserActivity>()
                .ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username));
        }
    }
}