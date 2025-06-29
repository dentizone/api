using FluentValidation;

namespace Dentizone.Application.DTOs.Q_A.AnswerDTO
{
    public class AnswerViewDto
    {
        public string Id { get; set; }
        public string ResponderName { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class AnswerViewDtoValidator : AbstractValidator<AnswerViewDto>
    {
        public AnswerViewDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.ResponderName).NotEmpty().WithMessage("ResponderName is required.");
            RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required.");
            RuleFor(x => x.CreatedAt).NotEmpty().WithMessage("CreatedAt is required.");
        }
    }
}