using FluentValidation;

namespace Dentizone.Application.DTOs.Q_A.AnswerDTO
{
    public class UpdateAnswerDto
    {
        public required string Text { get; set; }
    }

    public class UpdateAnswerDtoValidator : AbstractValidator<UpdateAnswerDto>
    {
        public UpdateAnswerDtoValidator()
        {
            RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required.");
        }
    }
}