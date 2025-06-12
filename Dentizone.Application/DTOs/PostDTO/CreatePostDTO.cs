using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.PostDTO
{
    public class CreatePostDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public PostItemCondition Condition { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string CateoryId { get; set; }
        public string SubCategoryId { get; set; }
        public DateTime? ExpireDate { get; set; }
        public List<string> AssetIds { get; set; }
        public List<int> DisplayOrder { get; set; } = new List<int>();

    }
    public class CreatePostDTOValidator : AbstractValidator<CreatePostDTO>
    {
        public CreatePostDTOValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.Condition).IsInEnum().WithMessage("Condition must be a valid enum value.");
            RuleFor(x => x.Street).NotEmpty().WithMessage("Street is required.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
            RuleFor(x => x.CateoryId).NotEmpty().WithMessage("Category ID is required.");
            RuleFor(x => x.SubCategoryId).NotEmpty().WithMessage("Subcategory ID is required.");

            RuleFor(x => x.AssetIds)
                .NotEmpty().WithMessage("At least one asset ID is required.")
                .Must(ids => ids.All(id => !string.IsNullOrEmpty(id)))
                .WithMessage("Asset IDs cannot be null or empty.");

            RuleFor(x => x.DisplayOrder)
                .NotEmpty().WithMessage("Display order is required.");
                
            RuleFor(x => x.ExpireDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("Expire date must be in the future if provided.");
        }
    }
}