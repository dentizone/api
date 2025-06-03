using AutoMapper;
using Dentizone.Application.DTOs.University;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper
{
    internal class UniversityProfile : Profile
    {
        public UniversityProfile()
        {
            CreateMap<SupportedUniversitiesDto, University>()
                .ReverseMap();
            CreateMap<CreateUniversityDto, University>().ReverseMap()
                                                        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                                                                                srcMember != null));
        }
    }
}