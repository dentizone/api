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
            RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required.");
        }
    }
}