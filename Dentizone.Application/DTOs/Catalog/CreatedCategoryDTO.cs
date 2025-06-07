using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dentizone.Application.DTOs.Catalog
{
    public class CreatedCategoryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class CreatedCategoryDTOValidator : AbstractValidator<CreatedCategoryDTO>
    {
        public CreatedCategoryDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");
        }
    }
}
