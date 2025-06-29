using AutoMapper;
using Dentizone.Application.DTOs.Withdrawal;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper
{
    public class WithdrawalProfile : Profile
    {
        public WithdrawalProfile()
        {
            CreateMap<WithdrawalRequestDto, WithdrawalRequest>().ReverseMap();
            CreateMap<WithdrawalRequest, WithdrawalRequestView>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Wallet.UserId.ToString()))
                .ReverseMap();
        }
    }
}