using FluentValidation;

namespace Dentizone.Application.DTOs.Catalog
{
    public class CategoryDTO
    {
        public string Name { get; set; }
    }

    public class CategoryDTOValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");
        }
    }
}