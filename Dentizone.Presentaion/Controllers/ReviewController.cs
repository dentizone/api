using Dentizone.Application.DTOs.Review;
using Dentizone.Application.Interfaces.Review;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ReviewController:ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderReview([FromBody] CreateReviewDTO createReviewDto)
        {
            var userId = createReviewDto.UserId;
            if (string.IsNullOrEmpty(userId))
                return BadRequest("UserId is required.");

            await _reviewService.CreateOrderReviewAsync(userId, createReviewDto);
            return Ok();
        }
        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(string reviewId, [FromBody] UpdateReviewDTO updateReviewDto)
        {
            await _reviewService.UpdateReviewAsync(reviewId, updateReviewDto);
            return Ok();
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(string reviewId)
        {
            await _reviewService.DeleteReviewAsync(reviewId);
            return Ok();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserReviews(string userId)
        {
            var reviews = await _reviewService.GetUserReviewsTaken(userId);
            return Ok(reviews);
        }
    }
}
