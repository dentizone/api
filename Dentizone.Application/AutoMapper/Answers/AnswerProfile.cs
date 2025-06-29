using AutoMapper;

namespace Dentizone.Application.AutoMapper.Answers
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<DTOs.Q_A.AnswerDTO.CreateAnswerDto, Domain.Entity.Answer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ReverseMap();
            CreateMap<DTOs.Q_A.AnswerDTO.UpdateAnswerDto, Domain.Entity.Answer>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ReverseMap();
            CreateMap<DTOs.Q_A.AnswerDTO.AnswerViewDto, Domain.Entity.Answer>()
                .ReverseMap();
        }
    }
}