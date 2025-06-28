using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dentizone.Application.DTOs.Q_A.QuestionDTO
{
    public class UpdateQuestionDto
    {
        public string Text { get; set; }
    }
    public class UpdateQuestionDtoValidator : AbstractValidator<UpdateQuestionDto>
    {
        public UpdateQuestionDtoValidator()
        {
            RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required.");
        }
    }
}
