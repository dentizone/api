using AutoMapper;
using Dentizone.Application.DTOs.Withdrawal;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper.Payments
{
    public class WithdrawalProfile : Profile
    {
        public WithdrawalProfile()
        {
            CreateMap<WithdrawalRequestDto, WithdrawalRequest>().ReverseMap();
            CreateMap<WithdrawalRequest, WithdrawalRequestView>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Wallet.UserId.ToString()))
                .ReverseMap();

            CreateMap<WithdrawalRequest, FullWithdrawalRequestView>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Wallet.User.FullName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Wallet.User.Email))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ReverseMap();
        }
    }
}