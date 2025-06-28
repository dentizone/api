using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dentizone.Application.DTOs.Q_A.QuestionDTO
{
    public class CreateQuestionDto
    {
        public string PostId { get; set; }
        public string Text { get; set; }
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
