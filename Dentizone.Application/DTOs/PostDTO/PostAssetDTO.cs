using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dentizone.Application.DTOs.PostDTO
{
    public class PostAssetDto
    {
        public string Id { get; set; }
        public string Url { get; set; }
    }

    public class PostAssetDtoValidator : AbstractValidator<PostAssetDto>
    {
        public PostAssetDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Asset ID is required.");
        }
    }
}