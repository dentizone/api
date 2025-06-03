using FluentValidation;

namespace Dentizone.Application.DTOs.University
{
    public class CreateUniversityDto : UniversityDto
    {
    }

    public class CreateUniversityValidation : AbstractValidator<CreateUniversityDto>
    {
        public CreateUniversityValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("University name is required.")
                .MaximumLength(100).WithMessage("University name cannot exceed 100 characters.");
            RuleFor(x => x.Domain)
                .NotEmpty().WithMessage("University domain is required.")
                .MaximumLength(100).WithMessage("University domain cannot exceed 100 characters.");
        }
    }
}