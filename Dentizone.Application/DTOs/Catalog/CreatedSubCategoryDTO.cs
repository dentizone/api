using FluentValidation;

namespace Dentizone.Application.DTOs.Catalog
{
    public class CreatedSubCategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class CreatedSubCategoryDtoValidator : AbstractValidator<CreatedSubCategoryDto>
    {
        public CreatedSubCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("SubCategory name is required.")
                .MaximumLength(100).WithMessage("SubCategory name cannot exceed 100 characters.");
        }
    }
}