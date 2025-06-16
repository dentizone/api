using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.Assets;

public class CreateAssetDto
{
    public required string Url { get; set; }
    public required long Size { get; set; }
    public required AssetType Type { get; set; }

    public required string UserId { get; set; }
}

public class CreateAssetDtoValidator : AbstractValidator<CreateAssetDto>
{
    public CreateAssetDtoValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Asset URL is required.")
            .MaximumLength(500).WithMessage("Asset URL cannot exceed 500 characters.");
        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("Asset size must be greater than zero.");
        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid asset type.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.")
            .MaximumLength(50).WithMessage("User ID cannot exceed 50 characters.");
    }
}