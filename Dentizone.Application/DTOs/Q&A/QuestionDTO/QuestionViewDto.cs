using Dentizone.Application.DTOs.Q_A.AnswerDTO;
using FluentValidation;

namespace Dentizone.Application.DTOs.Q_A.QuestionDTO
{
    public class QuestionViewDto
    {
        public required string Id { get; set; }
        public required string AskerName { get; set; }
        public required string Text { get; set; }
        public AnswerViewDto? Answer { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class QuestionViewDtoValidator : AbstractValidator<QuestionViewDto>
    {
        public QuestionViewDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.AskerName).NotEmpty().WithMessage("AskerName is required.");
            RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required.");
            RuleFor(x => x.CreatedAt).NotEmpty().WithMessage("CreatedAt is required.");
        }
    }
}