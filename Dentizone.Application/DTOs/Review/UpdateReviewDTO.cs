using FluentValidation;

namespace Dentizone.Application.DTOs.Review
{
    public class UpdateReviewDto
    {
        public string? Comment { get; set; }
        public int Stars { get; set; }
    }

    public class UpdateReviewDtoValidation : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewDtoValidation()
        {
            RuleFor(x => x.Comment)
                .MaximumLength(500)
                .When(x => x.Comment != null)
                .WithMessage("Comment must not exceed 500 characters when provided.");
            RuleFor(x => x.Stars)
                .InclusiveBetween(1, 5).WithMessage("Stars must be between 1 and 5.");
        }
    }
}