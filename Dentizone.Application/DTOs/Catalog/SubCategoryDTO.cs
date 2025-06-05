using FluentValidation;

namespace Dentizone.Application.DTOs.Catalog
{
    public class SubCategoryDTO
    {
        public string Name { get; set; }
    }

    public class SubCategoryDTOValidator : AbstractValidator<SubCategoryDTO>
    {
        public SubCategoryDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("SubCategory name is required.")
                .MaximumLength(100).WithMessage("SubCategory name cannot exceed 100 characters.");
        }
    }
}