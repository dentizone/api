using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper
{
    internal class UniversityProfile: Profile
    {
        public UniversityProfile()
        {
            CreateMap<SupportedUniversitiesDTO, University>()
                .ReverseMap();
            CreateMap<DeleteUniversityDTO, University>().ReverseMap();
            CreateMap<CreateUniversityDTO, University>() .ReverseMap();
            CreateMap<CreateUniversityDTO,University>().ReverseMap();
        }
    }
    
}
