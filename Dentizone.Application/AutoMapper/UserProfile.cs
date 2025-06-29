using AutoMapper;
using Dentizone.Application.DTOs.User;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, AppUser>()
                .ForPath(dest => dest.University.Name, opt => opt.MapFrom(src => src.UniversityName))
                .ReverseMap();

            CreateMap<UserView, UserDto>()
                .ReverseMap();

            CreateMap<CreateAppUser, AppUser>()
                .ReverseMap();

            CreateMap<KycStatusDto, AppUser>().ReverseMap();
            CreateMap<UserStateDto, AppUser>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            CreateMap<UserView, AppUser>()
                .ForPath(dest => dest.University.Name, opt => opt.MapFrom(src => src.UnversityName))
                .ReverseMap();

            CreateMap<DomainUserView, AppUser>()
                .ForPath(dest => dest.University.Name, opt => opt.MapFrom(src => src.UnversityName))
                .ReverseMap();
        }
    }
}