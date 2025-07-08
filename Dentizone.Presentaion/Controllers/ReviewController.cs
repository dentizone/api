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


        [HttpGet("all")]
        [Authorize("IsAdmin")]
        public async Task<IActionResult> GetAllReviews([FromQuery] ReviewFilterDto filter)
        {
            var reviews = await reviewService.GetAllReviewsAsync(filter);
            return Ok(reviews);
        }
    }
}