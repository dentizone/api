using AutoMapper;
using Dentizone.Application.DTOs.UserActivity;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper
{
    public class UserActivityProfile : Profile
    {
        public UserActivityProfile()
        {
            CreateMap<UserActivity, ActivityView>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ReverseMap();
            CreateMap<CreatedUserActivityDto, UserActivity>().ReverseMap();
            CreateMap<UserActivityDto, UserActivity>()
                .ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username));
        }
    }
}