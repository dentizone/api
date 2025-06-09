using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dentizone.Application.DTOs
{
    public class ForgetPasswordDTO
    {
        public string AcademicEmail { get; set; }
        public string NewPassword { get; set; }
    }
    public class ForgetPasswordDTOValidator :AbstractValidator<ForgetPasswordDTO>
    {
        public ForgetPasswordDTOValidator()
        {
            RuleFor(x => x.AcademicEmail)
                .NotEmpty().WithMessage("Academic email is required.")
                .EmailAddress().WithMessage("Invalid academic email format.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.edu(\.[a-z]{2})?$")
                .WithMessage("Academic email must end with '.edu' or a country-specific '.edu.xx' domain.");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(8).WithMessage("New password must be at least 8 characters long.");
        }
    }
}
