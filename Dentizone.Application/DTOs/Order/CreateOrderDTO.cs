using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Entity;
using FluentValidation;

namespace Dentizone.Application.DTOs.Order
{
    public class CreateOrderDTO
    {

        public List<string> PostIds { get; set; } = new List<string>();

    }
    public class CreateOrderDTOValidation : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderDTOValidation()
        {
           RuleFor(static x => x.PostIds)
                .NotEmpty().WithMessage("Posts list cannot be empty.")
                .Must(posts => posts.All(post => post != null)).WithMessage("All posts must be valid.");
        }
    }
}
