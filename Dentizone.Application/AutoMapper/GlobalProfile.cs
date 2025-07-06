using AutoMapper;
using Dentizone.Application.DTOs;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Application.AutoMapper
{
    public class GlobalProfile : Profile
    {
        public GlobalProfile()
        {


            CreateMap(typeof(PagedResult<>), typeof(PagedResultDto<>))
                                                        .ForMember("Items", opt => opt.MapFrom("Items"))
                                                        .ForMember("Page", opt => opt.MapFrom("Page"))
                                                        .ForMember("PageSize", opt => opt.MapFrom("PageSize"))
                                                        .ForMember("TotalCount", opt => opt.MapFrom("TotalCount"));


        }
    }
}
