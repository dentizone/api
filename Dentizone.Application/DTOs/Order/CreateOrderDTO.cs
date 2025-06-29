using FluentValidation;

namespace Dentizone.Application.DTOs.Order
{
    public class CreateOrderDto
    {
        public List<string> PostIds { get; set; } = new();
        public ShipInfoDto ShipInfo { get; set; } = new();
    }

    public class ShipInfoDtoValidation : AbstractValidator<ShipInfoDto>
    {
        public ShipInfoDtoValidation()
        {
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address cannot be empty.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.")
                .Matches(@"^[a-zA-Z0-9\s,.'-]{1,200}$").WithMessage("Address contains invalid characters.");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City cannot be empty.")
                .MaximumLength(100).WithMessage("City cannot exceed 100 characters.")
                .Matches(@"^[a-zA-Z\s-]{1,100}$").WithMessage("City contains invalid characters.");
        }
    }

    public class CreateOrderDtoValidation : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidation()
        {
            RuleFor(x => x.PostIds)
                .NotEmpty().WithMessage("Posts list cannot be empty.")
                .Must(ids => ids.Distinct().Count() == ids.Count)
                .WithMessage("Post IDs must be unique.");

            RuleForEach(x => x.PostIds)
                .NotEmpty().WithMessage("Post ID cannot be empty.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("Post ID must be a valid GUID.");

            RuleFor(x => x.ShipInfo)
                .NotNull().WithMessage("Shipping information cannot be null.")
                .SetValidator(new ShipInfoDtoValidation());
        }
    }
}