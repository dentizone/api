using FluentValidation;

namespace Dentizone.Application.DTOs.Review
{
    public class UpdateReviewDto
    {
        public string Comment { get; set; }
        public int Stars { get; set; }
    }

    public class UpdateReviewDtoValidation : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewDtoValidation()
        {
            RuleFor(x => x.Comment)
                .NotNull().WithMessage("Comment cannot be null.")
                .MaximumLength(500).WithMessage("Comment must not exceed 500 characters.");
            RuleFor(x => x.Stars)
                .InclusiveBetween(1, 5).WithMessage("Stars must be between 1 and 5.");
        }
    }
}