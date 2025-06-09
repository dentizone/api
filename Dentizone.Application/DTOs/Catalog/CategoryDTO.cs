using FluentValidation;

namespace Dentizone.Application.DTOs.Catalog
{
    public class CategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; } = string.Empty;
    }

    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");

            RuleFor(x => x.IconUrl)
                .NotEmpty().WithMessage("Icon URL is required.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Icon URL must be a valid absolute URL.");
        }
    }
}