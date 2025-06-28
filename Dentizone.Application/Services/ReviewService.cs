using AutoMapper;
using Dentizone.Application.DTOs.Review;
using Dentizone.Application.Interfaces.Order;
using Dentizone.Application.Interfaces.Review;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Services
{
    public class ReviewService(
        IHttpContextAccessor accessor,
        IMapper mapper,
        IReviewRepository repo,
        IOrderService orderService) : BaseService(accessor), IReviewService
    {
        public async Task CreateOrderReviewAsync(string userId, CreateReviewDto createReviewDto)
        {
            var review = new Review()
            {
                OrderId = createReviewDto.OrderId,
                Text = createReviewDto.Comment,
                UserId = userId
            };

            await repo.CreateAsync(review);
        }

        public async Task<bool> DeleteReviewAsync(string reviewId)
        {
            var review = await repo.GetByIdAsync(reviewId);
            if (review == null || review.IsDeleted)
                throw new NotFoundException("Review not found.");

            await repo.DeleteAsync(reviewId);
            return true;
        }

        public async Task<IEnumerable<ReviewDto>> GetSubmittedReviews(string userId)
        {
            var review = repo.FindAllBy(r => r.UserId == userId && !r.IsDeleted);


            return await review.Select(r => mapper.Map<ReviewDto>(r)).ToListAsync();
        }

        public async Task<bool> UpdateReviewAsync(string reviewId, UpdateReviewDto updateReviewDto)
        {
            var review = await repo.GetByIdAsync(reviewId);
            if (review == null || review.IsDeleted)
                throw new NotFoundException("Review not found.");

            review.Text = updateReviewDto.Comment;

            var updated = await repo.Update(review);

            if (updated == null)
            {
                throw new BadActionException("Failed to update review.");
            }

            return true;
        }

        public async Task<IEnumerable<ReviewDto>> GetReceivedReviews(string userId)
        {
            var reviews = await orderService.GetReviewedOrdersByUserId(userId);


            // Get the reviews for the orders
            var reviewDtos = reviews.Select(r => new ReviewDto
            {
                Comment = r.Review.Text ?? "No Comment",
                Stars = r.Review.Stars,
            });

            return reviewDtos.ToList();
        }

        protected override async Task<string> GetOwnerIdAsync(string resourceId)
        {
            var review = await repo.GetByIdAsync(resourceId);
            if (review == null)
            {
                throw new NotFoundException($"Review with id {resourceId} not found");
            }

            return review.UserId;
        }
    }
}