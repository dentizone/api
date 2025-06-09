using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
      
        public string AcademicEmail { get; set; }
        public string Password { get; set; }
        public string UniversityId { get; set; }
        public int AcademicYear { get; set; }

    }
    public class RegisterDTOValidator :AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");
            // email must contain '@' and a domain
            RuleFor(x => x.AcademicEmail)
            .NotEmpty().WithMessage("Academic email is required.")
            .EmailAddress().WithMessage("Invalid academic email format.")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.edu(\.[a-z]{2})?$")
            .WithMessage("Academic email must end with '.edu' or a country-specific '.edu.xx' domain.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8);

            RuleFor(x => x.UniversityId)
                .NotEmpty().WithMessage("University ID is required.");
            RuleFor(x => x.AcademicYear)
                .InclusiveBetween(1, 10).WithMessage("Academic year must be between 1 and 10.");
        }
    }
    
}
