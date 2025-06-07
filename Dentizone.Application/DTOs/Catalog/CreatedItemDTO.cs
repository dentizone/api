using FluentValidation;

namespace Dentizone.Application.DTOs.Catalog
{
    public class CreatedItemDTO
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
    }

    public class CreatedItemDTOValidator : AbstractValidator<CreatedItemDTO>
    {
        public CreatedItemDTOValidator()
        {

            RuleFor(b => b.CategoryId)
                .NotEmpty().WithMessage("Category ID is required.");

            RuleFor(b => b.SubCategoryId)
                .NotEmpty().WithMessage("SubCategory ID is required.");
        }
    }
}