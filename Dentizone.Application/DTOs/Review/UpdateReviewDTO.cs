using FluentValidation;

namespace Dentizone.Application.DTOs.Review
{
    public class UpdateReviewDto
    {
        public string Comment { get; set; }
    }

    public class UpdateReviewDtoValidation : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewDtoValidation()
        {
            RuleFor(x => x.Comment)
                .NotNull().WithMessage("Comment cannot be null.")
                .MaximumLength(500).WithMessage("Comment must not exceed 500 characters.");
        }
    }
}