using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dentizone.Application.DTOs
{
    public class RestPasswordDTO
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
    public class RestPasswordDTOValidator :AbstractValidator<RestPasswordDTO>
    {
        public RestPasswordDTOValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Current password is required.")
                .MinimumLength(8).WithMessage("Current password must be at least 8 characters long.");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(8).WithMessage("New password must be at least 8 characters long.");
        }
    }
}
