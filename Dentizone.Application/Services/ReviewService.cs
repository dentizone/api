using AutoMapper;
using Dentizone.Application.DTOs.Review;
using Dentizone.Application.Interfaces.Review;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Application.Services
{
    public class ReviewService(IMapper mapper,IReviewRepository repo) : IReviewService
    {
        public async Task CreateOrderReviewAsync(string userId, CreateReviewDTO createReviewDto)
        {
            createReviewDto.UserId = userId;

            var review = mapper.Map<Review>(createReviewDto);

            review.CreatedAt = DateTime.UtcNow;
            review.UpdatedAt = DateTime.UtcNow;
            review.IsDeleted = false;

            await repo.CreateAsync(review);

        }

        public async Task DeleteReviewAsync(string reviewId)
        {
            await repo.DeleteAsync(reviewId);
        }

        public async Task<IEnumerable<ReviewDTO>> GetUserReviewsTaken(string userId)
        {
            var review = await repo.FindBy(r => r.UserId == userId && !r.IsDeleted);

            if (review == null)
                return Enumerable.Empty<ReviewDTO>();

            return new List<ReviewDTO> { mapper.Map<ReviewDTO>(review) };
        }

        public async Task UpdateReviewAsync(string reviewId, UpdateReviewDTO updateReviewDto)
        {
            var review = await repo.GetByIdAsync(reviewId);
            if (review == null || review.IsDeleted)
                throw new KeyNotFoundException("Review not found.");

            // Update fields
            review.Text = updateReviewDto.Comment;
            review.UpdatedAt = DateTime.UtcNow;

            await repo.Update(review);
        }
    }
}
