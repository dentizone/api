using FluentValidation;

namespace Dentizone.Application.DTOs.Catalog
{
    public class CategoryDto
    {
        public string Name { get; set; }
    }

    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");
        }
    }
}