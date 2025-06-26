using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Application.DTOs.Review
{
    public class UpdateReviewDTO
    {
        public string Comment { get; set; }
    }
    public class UpdateReviewDtoValidation : AbstractValidator<UpdateReviewDTO>
    {
        public UpdateReviewDtoValidation()
        {
            RuleFor(x => x.Comment)
                .NotNull().WithMessage("Comment cannot be null.")
                .MaximumLength(500).WithMessage("Comment must not exceed 500 characters.");
        }
    }
}
