using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.User
{
    public class UserDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public int AcademicYear { get; set; }
        public long? NationalId { get; set; }
        public KycStatus KycStatus { get; set; }
        public UserState Status { get; set; }
        public bool IsDeleted { get; set; }
        public string UniversityId { get; set; }
    }
    public class UserDTOValidator : AbstractValidator<UserDTO>
    { 
        public UserDTOValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.AcademicYear).GreaterThan(0).WithMessage("Academic year must be greater than 0.");
            RuleFor(x => x.NationalId).GreaterThan(0).When(x => x.NationalId.HasValue)
                .WithMessage("National ID must be greater than 0 if provided.");
            RuleFor(x => x.KycStatus).IsInEnum().WithMessage("KycStatus must be a valid enum value.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("Status must be a valid enum value.");
            RuleFor(x => x.UniversityId).NotEmpty().WithMessage("University ID is required.");
        }
    }
    }
