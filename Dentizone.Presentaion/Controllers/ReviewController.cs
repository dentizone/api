using Dentizone.Application.DTOs.Review;
using Dentizone.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dentizone.Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController(IReviewService reviewService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Policy = "IsVerified")]

        public async Task<IActionResult> CreateOrderReview([FromBody] CreateReviewDto createReviewDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await reviewService.CreateOrderReviewAsync(userId, createReviewDto);
            return Ok();
        }

        [HttpPut("{reviewId}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> UpdateReview(string reviewId, [FromBody] UpdateReviewDto updateReviewDto)
        {
            await reviewService.UpdateReviewAsync(reviewId, updateReviewDto);
            return Ok();
        }

        [HttpDelete("{reviewId}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> DeleteReview(string reviewId)
        {
            await reviewService.DeleteReviewAsync(reviewId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetSubmittedReviews()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reviews = await reviewService.GetSubmittedReviews(userId);
            return Ok(reviews);
        }

        [HttpGet("received-review")]
        public async Task<IActionResult> GetReceivedReviews()
        {
            var reviews = await reviewService.GetReceivedReviews(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(reviews);
        }
        [HttpGet("all")]
        [Authorize("IsAdmin")]
        public async Task<IActionResult> GetAllReviews([FromQuery] int page)
        {
            var reviews = await reviewService.GetAllReviewsAsync(page);
            return Ok(reviews);
        }
    }
}