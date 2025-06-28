using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs.Q_A.AnswerDTO;
using Dentizone.Application.DTOs.Q_A.QuestionDTO;

namespace Dentizone.Application.Interfaces
{
    public interface IQAService
    {
        Task<QuestionViewDto> AskQuestionAsync(CreateQuestionDto dto, string askerId);
        Task<IEnumerable<QuestionViewDto>> GetQuestionsForPostAsync(string postId);
        Task<AnswerViewDto> AnswerQuestionAsync(string questionId, CreateAnswerDto dto, string responderId);
        Task UpdateQuestionAsync(string questionId, UpdateQuestionDto dto);
        Task DeleteQuestionAsync(string questionId);
        Task UpdateAnswerAsync(string answerId, UpdateAnswerDto dto);
        Task DeleteAnswerAsync(string answerId);
    }
}
