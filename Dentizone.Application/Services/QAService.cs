using System.Linq.Expressions;
using AutoMapper;
using Dentizone.Application.DTOs.Q_A.AnswerDTO;
using Dentizone.Application.DTOs.Q_A.QuestionDTO;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Application.Services
{
    public class QaService(IMapper mapper, IAnswerRepository answerRepository, IQuestionRepository questionRepository)
        : IQaService
    {
        public async Task<AnswerViewDto> AnswerQuestionAsync(string questionId, CreateAnswerDto dto, string responderId)
        {
            var question = await questionRepository.GetByIdAsync(questionId);
            if (question == null)
            {
                throw new NotFoundException("Question not found");
            }

            var post = question.Post;
            if (post == null)
            {
                throw new NotFoundException("Post not found");
            }

            if (responderId != post.SellerId)
            {
                throw new UnauthorizedAccessException("You are not authorized to answer this question");
            }

            var existingAnswer = await answerRepository.FindBy(a => a.QuestionId == questionId && !a.IsDeleted);
            if (existingAnswer != null)
            {
                throw new BadActionException("This question has already been answered.");
            }

            var answer = mapper.Map<Answer>(dto);
            question.Status = QuestionStatus.Answered;
            await answerRepository.CreateAsync(answer);
            await questionRepository.UpdateAsync(question);
            return mapper.Map<AnswerViewDto>(answer);
        }

        public async Task<QuestionViewDto> AskQuestionAsync(CreateQuestionDto dto, string askerId)
        {
            var post = questionRepository.GetByIdAsync(dto.PostId);
            if (post == null)
            {
                throw new NotFoundException("Post not found");
            }

            var question = mapper.Map<Question>(dto);
            question.AskerId = askerId;
            question.Status = QuestionStatus.Unanswered;
            await questionRepository.CreateAsync(question);
            return mapper.Map<QuestionViewDto>(question);
        }

        public async Task DeleteAnswerAsync(string answerId)
        {
            var answer = await answerRepository.GetByIdAsync(answerId);
            var question = await questionRepository.GetByIdAsync(answer.QuestionId);
            var post = question.Post;
            if (post == null)
            {
                throw new ArgumentException("Post not found for the question", nameof(answerId));
            }

            await answerRepository.DeleteAsync(answerId);
            question.Status = QuestionStatus.Unanswered;
            await questionRepository.UpdateAsync(question);
        }

        public async Task DeleteQuestionAsync(string questionId)
        {
            var question = await questionRepository.GetByIdAsync(questionId);
            if (question == null)
            {
                throw new ArgumentException("Question not found", nameof(questionId));
            }

            var post = question.Post;
            if (post == null)
            {
                throw new ArgumentException("Post not found for the question", nameof(questionId));
            }

            await questionRepository.DeleteAsync(questionId);
        }

        public async Task<IEnumerable<QuestionViewDto>> GetQuestionsForPostAsync(string postId)
        {
            var includes = new Expression<Func<Question, object>>[]
            {
                q => q.Answer
            };

            var questions = await questionRepository.FindAllBy(
                q => q.PostId == postId && !q.IsDeleted,
                includes);


            return mapper.Map<IEnumerable<QuestionViewDto>>(questions);
        }

        public async Task UpdateAnswerAsync(string answerId, UpdateAnswerDto dto)
        {
            var answer = await answerRepository.GetByIdAsync(answerId);
            answer.Text = dto.Text;
            await answerRepository.UpdateAsync(answer);
        }

        public async Task UpdateQuestionAsync(string questionId, UpdateQuestionDto dto)
        {
            var question = await questionRepository.GetByIdAsync(questionId);
            if (question == null)
            {
                throw new ArgumentException("Question not found", nameof(questionId));
            }

            question.Text = dto.Text;
            await questionRepository.UpdateAsync(question);
        }
    }
}