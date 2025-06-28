using Dentizone.Application.DTOs.Review;

namespace Dentizone.Application.Interfaces.Review
{
    public interface IReviewService
    {
        Task CreateOrderReviewAsync(string userId, CreateReviewDto createReviewDto);
        Task UpdateReviewAsync(string reviewId, UpdateReviewDto updateReviewDto);
        Task DeleteReviewAsync(string reviewId);
        Task<IEnumerable<ReviewDto>> GetSubmittedReviews(string userId);
    }
}