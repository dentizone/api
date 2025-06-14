using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.PostFilterDTO
{
    public class UserPreferenceDTO
    {
        public string? Keyword { get; set; }
        public string? City { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public PostItemCondition? Condition { get; set; } 
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SortBy { get; set; } 
        public bool SortDirection { get; set; }
        public int PageNumber { get; set; }

    }
    public class  UserPreferenceDTOValidator: AbstractValidator<UserPreferenceDTO>
    {
        public UserPreferenceDTOValidator()
        {
            RuleFor(x => x.SortDirection).NotNull().WithMessage("SortDirection cannot be null.");
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("PageNumber must be greater than 0.");
        }

    }
}
