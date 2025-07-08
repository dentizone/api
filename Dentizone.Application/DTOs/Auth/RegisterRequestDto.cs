using FluentValidation;

namespace Dentizone.Application.DTOs.Auth;

public class RegisterRequestDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string FullName { get; set; }
    public required string Username { get; set; }
    public int AcademicYear { get; set; }
    public required string UniversityId { get; set; }
    public long PhoneNumber { get; set; }
}

public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.AcademicYear).GreaterThan(0).LessThanOrEqualTo(5)
            .WithMessage("Academic year must be greater than 0 and less than 6.");
        RuleFor(x => x.UniversityId).NotEmpty().WithMessage("University ID is required.");
        RuleFor(x => x.PhoneNumber)
            .GreaterThan(0).WithMessage("Phone number must be a positive number.")
            ;
    }
}