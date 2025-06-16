using FluentValidation;

namespace Dentizone.Application.DTOs.Cart
{
    public class AddToCartDto
    {
        public required string PostId { get; set; }
    }

    public class AddToCartDtoValidation : AbstractValidator<AddToCartDto>
    {
        public AddToCartDtoValidation()
        {
            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("Post ID is required.")
                .NotNull().WithMessage("Post ID cannot be null.");
        }
    }
}