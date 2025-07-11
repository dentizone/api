using AutoMapper;
using Dentizone.Application.DTOs.Q_A.AnswerDTO;
using Dentizone.Application.DTOs.Q_A.QuestionDTO;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Dentizone.Application.Services
{
    public class QaService(
        IMapper mapper,
        IAnswerRepository answerRepository,
        IQuestionRepository questionRepository,
        IBackgroundJobService _backgroundJob,
        IUserActivityService userActivity)
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
            answer.QuestionId = questionId;
            await answerRepository.CreateAsync(answer);
            await questionRepository.UpdateAsync(question);

            _backgroundJob.Enqueue<IMonitorJob>(job =>
                job.ReviewAnswerAsync(answer.Id, answer.Text));
            await userActivity.CreateAsync(UserActivities.AnsweredQuestion, DateTime.UtcNow, responderId);
            return mapper.Map<AnswerViewDto>(answer);
        }

        public async Task<QuestionViewDto> AskQuestionAsync(CreateQuestionDto dto, string askerId)
        {
            var question = mapper.Map<Question>(dto);
            question.AskerId = askerId;
            question.Status = QuestionStatus.Unanswered;
            await questionRepository.CreateAsync(question);

            _backgroundJob.Enqueue<IMonitorJob>(job =>
                job.ReviewQuestionAsync(question.Id, question.Text));

            await userActivity.CreateAsync(UserActivities.AskedQuestion, DateTime.UtcNow, askerId);

            return mapper.Map<QuestionViewDto>(question);
        }

        public async Task DeleteAnswerAsync(string answerId)
        {
            var answer = await answerRepository.GetByIdAsync(answerId);
            if (answer == null)
            {
                throw new NotFoundException("No Answer with this ID");
            }

            var question = await questionRepository.GetByIdAsync(answer.QuestionId);
            if (question == null)
            {
                throw new NotFoundException("Question not found for the answer");
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
                throw new NotFoundException("Question not found");
            }

            var post = question.Post;
            if (post == null)
            {
                throw new NotFoundException("Post not found for the question");
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
            var answer = await answerRepository.GetByIdAsync(answerId) ??
                         throw new NotFoundException("No Answer with this ID");
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