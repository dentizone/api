using AutoMapper;
using Dentizone.Application.DTOs.University;
using Dentizone.Domain.Entity;

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
        }
    }
}