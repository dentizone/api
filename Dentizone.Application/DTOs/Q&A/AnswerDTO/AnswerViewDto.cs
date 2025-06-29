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
            RuleFor(x => x.ResponderName)
                .NotEmpty().WithMessage("ResponderName is required.")
                .MaximumLength(100).WithMessage("ResponderName cannot exceed 100 characters.");
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Text is required.")
                .MaximumLength(500).WithMessage("Text cannot exceed 500 characters.");
            RuleFor(x => x.CreatedAt)
                .Must(date => date != default).WithMessage("CreatedAt must be a valid date.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreatedAt cannot be in the future.");
        }
    }
}