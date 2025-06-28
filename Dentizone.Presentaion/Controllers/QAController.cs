using System.Runtime.CompilerServices;
using System.Security.Claims;
using Dentizone.Application.DTOs.Q_A.AnswerDTO;
using Dentizone.Application.DTOs.Q_A.QuestionDTO;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QAController(IQAService iQAService) : ControllerBase
    {
        [Authorize]
        [HttpPost("/api/questions")]
        public IActionResult AskQuestion([FromBody] CreateQuestionDto dto)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (dto == null || string.IsNullOrEmpty(dto.PostId) || string.IsNullOrEmpty(dto.Text))
            {
                return BadRequest("Invalid question data.");
            }
            var question = iQAService.AskQuestionAsync(dto, userId).Result;
            return Ok(question);
        }
        [HttpGet("/api/posts/{postId}/questions")]
        public async Task<IActionResult> GetQuestionsForPost(string postId)
        {
            var questions = await iQAService.GetQuestionsForPostAsync(postId);
            return Ok(questions);
        }

        [Authorize]
        [HttpPost("/api/questions/{questionId}/answers")]
        public async Task<IActionResult> AnswerQuestion(string questionId, [FromBody] CreateAnswerDto dto)
        {
            var responderId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (responderId == null)
                return Unauthorized();

            var answer = await iQAService.AnswerQuestionAsync(questionId, dto, responderId);
            return Ok(answer);
        }

        [Authorize]
        [HttpPut("/api/questions/{questionId}")]
        public async Task<IActionResult> UpdateQuestion(string questionId, [FromBody] UpdateQuestionDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Text))
            {
                return BadRequest("Invalid question data.");
            }
            await iQAService.UpdateQuestionAsync(questionId, dto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("/api/questions/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(string questionId)
        {
            await iQAService.DeleteQuestionAsync(questionId);
            return NoContent();
        }

        [Authorize]
        [HttpPut("/api/answers/{answerId}")]
        public async Task<IActionResult> UpdateAnswer(string answerId, [FromBody] UpdateAnswerDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Text))
            {
                return BadRequest("Invalid answer data.");
            }
            await iQAService.UpdateAnswerAsync(answerId, dto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("/api/answers/{answerId}")]
        public async Task<IActionResult> DeleteAnswer(string answerId)
        {
            await iQAService.DeleteAnswerAsync(answerId);
            return NoContent();
        }
    }
}
