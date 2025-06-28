using Dentizone.Application.DTOs.Review;

namespace Dentizone.Application.Interfaces.Review
{
    public interface IReviewService
    {
        Task CreateOrderReviewAsync(string userId, CreateReviewDto createReviewDto);
        Task<bool> UpdateReviewAsync(string reviewId, UpdateReviewDto updateReviewDto);
        Task<bool> DeleteReviewAsync(string reviewId);
        Task<IEnumerable<ReviewDto>> GetSubmittedReviews(string userId);
        Task<IEnumerable<ReviewDto>> GetReceivedReviews(string userId);
    }
}