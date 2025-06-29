using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.User
{
    public class UserStateDto
    {
        public UserState Status { get; set; }
    }

    public class UserStateDtoValidator : AbstractValidator<UserStateDto>
    {
        public UserStateDtoValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status must be a valid enum value.");
        }
    }
}