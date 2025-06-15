using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dentizone.Application.DTOs.Cart
{
    public class CartItemDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PostId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Url { get; set; }
    }

    public class CartItemDTOValidation  : AbstractValidator<CartItemDTO>
    {
        public CartItemDTOValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Cart item ID is required.");

            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("Post ID is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Image URL is required.");
                
        }

        
    }
}
