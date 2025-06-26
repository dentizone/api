using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Application.DTOs.Review
{
    public class CreateReviewDTO
    {
        public string ID { get; set; }
        public string UserId { get; set; }
        public string OrderItemId { get; set; }
        public decimal Stars { get; set; }
        public string Comment { get; set; }

    }
    public class CreateReviewDtoValidation : AbstractValidator<CreateReviewDTO>
    {
        public CreateReviewDtoValidation()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .NotNull().WithMessage("User ID cannot be null.");

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
