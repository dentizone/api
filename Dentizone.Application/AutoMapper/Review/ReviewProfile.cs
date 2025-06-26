using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs.Review;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper.Review
{
    internal class ReviewProfile:Profile
    {
        public ReviewProfile()
        {
            CreateMap<Dentizone.Domain.Entity.Review, Dentizone.Application.DTOs.Review.CreateReviewDTO>().ReverseMap();
            CreateMap<Dentizone.Domain.Entity.Review, Dentizone.Application.DTOs.Review.UpdateReviewDTO>().ReverseMap();
            CreateMap<Dentizone.Domain.Entity.Review, Dentizone.Application.DTOs.Review.ReviewDTO>().ReverseMap();
        }
    }
    
}
