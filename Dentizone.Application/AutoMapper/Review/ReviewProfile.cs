using AutoMapper;

namespace Dentizone.Application.AutoMapper.Review
{
    internal class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Dentizone.Domain.Entity.Review, Dentizone.Application.DTOs.Review.CreateReviewDto>().ReverseMap();
            CreateMap<Dentizone.Domain.Entity.Review, Dentizone.Application.DTOs.Review.UpdateReviewDto>().ReverseMap();
            CreateMap<Dentizone.Domain.Entity.Review, Dentizone.Application.DTOs.Review.ReviewDto>().ReverseMap();
        }
    }
}