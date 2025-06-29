using FluentValidation;

namespace Dentizone.Application.DTOs.Q_A.QuestionDTO
{
    #nullable enable
    public class CreateQuestionDto
    {
        public Guid PostId { get; set; }
        public string? Text { get; set; }
    }

    public class CreateQuestionDtoValidator : AbstractValidator<CreateQuestionDto>
    {
        public CreateQuestionDtoValidator()
        {
            RuleFor(x => x.PostId).NotEmpty().WithMessage("PostId is required.");
            RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required.");
        }
    }
}