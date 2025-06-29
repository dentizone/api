using AutoMapper;
using Dentizone.Application.DTOs.Payment;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper.Payments
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentView>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(src => src.Buyer.FullName))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ReverseMap();
        }
    }
}