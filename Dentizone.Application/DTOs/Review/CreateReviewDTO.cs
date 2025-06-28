using FluentValidation;

namespace Dentizone.Application.DTOs.Review
{
    public class CreateReviewDto
    {
        public string OrderItemId { get; set; }
        public decimal Stars { get; set; }
        public string Comment { get; set; }
    }

    public class CreateReviewDtoValidation : AbstractValidator<CreateReviewDto>
    {
        public CreateReviewDtoValidation()
        {
            RuleFor(x => x.OrderItemId)
                .NotEmpty().WithMessage("Order Item ID is required.")
                .NotNull().WithMessage("Order Item ID cannot be null.");

            RuleFor(x => x.Stars)
                .InclusiveBetween(1, 5).WithMessage("Stars must be between 1 and 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(500).WithMessage("Comment must not exceed 500 characters.");
        }
    }
}