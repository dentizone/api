using FluentValidation;

namespace Dentizone.Application.DTOs.Catalog
{
    public class SubCategoryDto
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
    }

    public class SubCategoryDtoValidator : AbstractValidator<SubCategoryDto>
    {
        public SubCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("SubCategory name is required.")
                .MaximumLength(100).WithMessage("SubCategory name cannot exceed 100 characters.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category ID is required.")
                .WithMessage("Invalid Category ID.");
        }
    }
}