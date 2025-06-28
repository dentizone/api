using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dentizone.Application.AutoMapper.Answers
{
    public class AnswerProfile :Profile
    {
        public AnswerProfile()
        {
            CreateMap<Dentizone.Application.DTOs.Q_A.AnswerDTO.CreateAnswerDto, Dentizone.Domain.Entity.Answer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ReverseMap();
            CreateMap<Dentizone.Application.DTOs.Q_A.AnswerDTO.UpdateAnswerDto, Dentizone.Domain.Entity.Answer>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ReverseMap();
            CreateMap<Dentizone.Application.DTOs.Q_A.AnswerDTO.AnswerViewDto, Dentizone.Domain.Entity.Answer>().ReverseMap();
        }
    }
   
}
