using FluentValidation;

namespace Dentizone.Application.DTOs.University
{
    public class CreateUniversityDto
    {
        public string Name { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
    }

    public class CreateUniversityValidation : AbstractValidator<CreateUniversityDto>
    {
        public CreateUniversityValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("University name is required.")
                .MaximumLength(100).WithMessage("University name cannot exceed 100 characters.");
            RuleFor(x => x.Domain)
                .MaximumLength(48).WithMessage("University domain cannot exceed 48 characters.")
                .Matches(@"^[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.edu$")
                .WithMessage("University domain must be a valid .edu domain format.");
        }
    }
}