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
    [Authorize]
    public class QaController(IQaService qaService) : ControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> AskQuestion([FromBody] CreateQuestionDto dto)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var question = await qaService.AskQuestionAsync(dto, userId);
            return Ok(question);
        }

        [HttpGet("questions/{postId}")]
        public async Task<IActionResult> GetQuestionsForPost(string postId)
        {
            var questions = await qaService.GetQuestionsForPostAsync(postId);
            return Ok(questions);
        }

        [HttpPost("answer/{questionId}")]
        public async Task<IActionResult> AnswerQuestion(string questionId, [FromBody] CreateAnswerDto dto)
        {
            var responderId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var answer = await qaService.AnswerQuestionAsync(questionId, dto, responderId);
            return Ok(answer);
        }

        [HttpPut("{questionId}")]
        public async Task<IActionResult> UpdateQuestion(string questionId, [FromBody] UpdateQuestionDto dto)
        {
            await qaService.UpdateQuestionAsync(questionId, dto);
            return NoContent();
        }

        [HttpDelete("{questionId}")]
        public async Task<IActionResult> DeleteQuestion(string questionId)
        {
            await qaService.DeleteQuestionAsync(questionId);
            return NoContent();
        }

        [HttpPut("answers/{answerId}")]
        public async Task<IActionResult> UpdateAnswer(string answerId, [FromBody] UpdateAnswerDto dto)
        {
            await qaService.UpdateAnswerAsync(answerId, dto);
            return NoContent();
        }

        [HttpDelete("answers/{answerId}")]
        public async Task<IActionResult> DeleteAnswer(string answerId)
        {
            await qaService.DeleteAnswerAsync(answerId);
            return NoContent();
        }
    }
}