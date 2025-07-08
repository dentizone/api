using AutoMapper;
using Dentizone.Application.DTOs;
using Dentizone.Application.DTOs.Review;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace Dentizone.Application.Services
{
    public class ReviewService(
        IHttpContextAccessor accessor,
        IMapper mapper,
        IReviewRepository repo,
        IOrderService orderService,
        IBackgroundJobService backgroundJob) : BaseService(accessor), IReviewService
    {
        public async Task CreateOrderReviewAsync(string userId, CreateReviewDto createReviewDto)
        {
            var order = await orderService.GetOrderByIdAsync(createReviewDto.OrderId, userId);

            if (order == null)
            {
                throw new NotFoundException("Order with provided id is not found");
            }

            var review = new Review()
            {
                OrderId = createReviewDto.OrderId,
                Text = createReviewDto.Comment,
                UserId = userId
            };

            await repo.CreateAsync(review);
            await orderService.MarkOrderAsReviewed(order.Id);
            backgroundJob.Enqueue<IMonitorJob>(job => job.ReviewReviewAsync(review.Id, review.Text));
        }

        public async Task DeleteReviewAsync(string reviewId)
        {
            await AuthorizeAdminOrOwnerAsync(reviewId);
            await repo.DeleteAsync(reviewId);
        }

        public async Task<IEnumerable<ReviewDto>> GetSubmittedReviews(string userId)
        {
            return [];
        }

        public async Task<bool> UpdateReviewAsync(string reviewId, UpdateReviewDto updateReviewDto)
        {
            await AuthorizeAdminOrOwnerAsync(reviewId);
            var review = await repo.FindBy(r => r.Id == reviewId && !r.IsDeleted) ??
                         throw new NotFoundException("Review with Provided id is not found");

            if (updateReviewDto.Comment != null)
            {
                review.Text = updateReviewDto.Comment;
            }

            if (updateReviewDto.Stars != null)
            {
                review.Stars = updateReviewDto.Stars.Value;
            }

            if (updateReviewDto.Sentiment != null)
            {
                updateReviewDto.Sentiment = updateReviewDto.Sentiment;
            }


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

        public async Task<PagedResultDto<ReviewView>> GetAllReviewsAsync(int page)
        {
            var reviews = await repo.FindAllBy(page, null);

            return mapper.Map<PagedResultDto<ReviewView>>(reviews);
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