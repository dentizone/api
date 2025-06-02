using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dentizone.Application.DTOs.CatalogDTOs
{
    public class CreatedItemDTO
    {
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
