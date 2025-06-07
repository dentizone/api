using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
