using AutoMapper;
using Dentizone.Application.DTOs.Review;
using Dentizone.Application.Interfaces.Review;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Services
{
    public class ReviewService(IMapper mapper, IReviewRepository repo) : IReviewService
    {
        public async Task CreateOrderReviewAsync(string userId, CreateReviewDto createReviewDto)
        {
            var review = mapper.Map<Review>(createReviewDto);
            await repo.CreateAsync(review);
        }

        public async Task DeleteReviewAsync(string reviewId)
        {
            await repo.DeleteAsync(reviewId);
        }

        public async Task<IEnumerable<ReviewDto>> GetUserReviewsTaken(string userId)
        {
            var review = repo.FindAllBy(r => r.UserId == userId && !r.IsDeleted);


            return await review.Select(r => mapper.Map<ReviewDto>(r)).ToListAsync();
        }

        public async Task UpdateReviewAsync(string reviewId, UpdateReviewDto updateReviewDto)
        {
            var review = await repo.GetByIdAsync(reviewId);
            if (review == null || review.IsDeleted)
                throw new NotFoundException("Review not found.");

            review.Text = updateReviewDto.Comment;

            await repo.Update(review);
        }
    }
}