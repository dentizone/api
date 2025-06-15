using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dentizone.Application.DTOs.Cart
{
    public class AddToCartDTO
    {
        public string PostId { get; set; }
    }
    public class AddToCartDTOValidation : AbstractValidator<AddToCartDTO>
    {
        public AddToCartDTOValidation() 
        {
            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("Post ID is required.");
        }
    }

}
