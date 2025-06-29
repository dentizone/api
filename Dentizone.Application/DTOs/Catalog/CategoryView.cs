using FluentValidation;

namespace Dentizone.Application.DTOs.Catalog
{
    public class CategoryView
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
    }

    public class CreatedCategoryDtoValidator : AbstractValidator<CategoryView>
    {
        public CreatedCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");
        }
    }
}