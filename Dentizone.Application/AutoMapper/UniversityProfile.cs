using AutoMapper;
using Dentizone.Application.DTOs;
using Dentizone.Application.DTOs.University;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Application.AutoMapper
{
    public class UniversityProfile : Profile
    {
        public UniversityProfile()
        {
            CreateMap<UniversityDto, University>()
                .ReverseMap();
            CreateMap<UpdateUniversityDto, University>()
                .ReverseMap();
            CreateMap<UniversityView, University>()
                .ReverseMap();
            CreateMap<SupportedUniversitiesDto, University>()
                .ReverseMap();
            CreateMap<CreateUniversityDto, University>().ReverseMap()
                                                        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                                                                                srcMember != null));

            CreateMap(typeof(PagedResult<>), typeof(PagedResultDto<>))
                .ForMember("Items", opt => opt.MapFrom("Items"))
                .ForMember("Page", opt => opt.MapFrom("Page"))
                .ForMember("PageSize", opt => opt.MapFrom("PageSize"))
                .ForMember("TotalCount", opt => opt.MapFrom("TotalCount"));
        }
    }
}