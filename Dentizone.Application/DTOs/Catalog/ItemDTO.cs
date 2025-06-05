using FluentValidation;

namespace Dentizone.Application.DTOs.Catalog
{
    public class ItemDTO
    {
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
    }

    public class ItemDTOValidator : AbstractValidator<ItemDTO>
    {
        public ItemDTOValidator()
        {
            RuleFor(b => b.CategoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

            RuleFor(b => b.SubCategoryName)
                .NotEmpty().WithMessage("SubCategory name is required.")
                .MaximumLength(100).WithMessage("SubCategory name must not exceed 100 characters.");
        }
    }
}