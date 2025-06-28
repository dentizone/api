using System.Security.Claims;
using Dentizone.Application.DTOs.Review;
using Dentizone.Application.Interfaces.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderReview([FromBody] CreateReviewDto createReviewDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _reviewService.CreateOrderReviewAsync(userId, createReviewDto);
            return Ok();
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(string reviewId, [FromBody] UpdateReviewDto updateReviewDto)
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

        [HttpGet]
        public async Task<IActionResult> GetUserReviews()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reviews = await _reviewService.GetUserReviewsTaken(userId);
            return Ok(reviews);
        }
    }
}