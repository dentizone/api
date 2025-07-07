using AutoMapper;
using Dentizone.Application.DTOs.Review;

namespace Dentizone.Application.AutoMapper.Review
{
    internal class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Dentizone.Domain.Entity.Review, DTOs.Review.CreateReviewDto>().ReverseMap();
            CreateMap<Dentizone.Domain.Entity.Review, DTOs.Review.UpdateReviewDto>().ReverseMap();
            CreateMap<Dentizone.Domain.Entity.Review, DTOs.Review.ReviewDto>().ReverseMap();

            CreateMap<Domain.Entity.Review, ReviewView>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.OrderViewDto, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Stars, opt => opt.MapFrom(src => src.Stars))
                .ReverseMap();
        }
    }
}