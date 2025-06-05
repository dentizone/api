using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.Asset;

public class CreateAssetDto
{
    public string Url { get; set; }
    public long Size { get; set; }
    public AssetType Type { get; set; }
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
    }
}