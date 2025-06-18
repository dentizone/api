using FluentValidation;

namespace Dentizone.Application.DTOs.Catalog
{
    public class CreatedSubCategoryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class CreatedSubCategoryDTOValidator : AbstractValidator<CreatedSubCategoryDTO>
    {
        public CreatedSubCategoryDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("SubCategory name is required.")
                .MaximumLength(100).WithMessage("SubCategory name cannot exceed 100 characters.");
        }
    }
}