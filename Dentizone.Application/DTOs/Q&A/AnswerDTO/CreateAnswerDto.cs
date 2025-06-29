using FluentValidation;

namespace Dentizone.Application.DTOs.Q_A.AnswerDTO
{
    public class CreateAnswerDto
    {
        public string Text { get; set; }
    }

    public class CreateAnswerDtoValidator : AbstractValidator<CreateAnswerDto>
    {
        public CreateAnswerDtoValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Text is required.")
                .MaximumLength(500).WithMessage("Text cannot exceed 500 characters.")
                .Must(text => !string.IsNullOrWhiteSpace(text)).WithMessage("Text cannot be whitespace only.");
        }
    }
}