using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.User
{
    public class KycStatusDto
    {
        public KycStatus KycStatus { get; set; }
    }

    public class KycStatusDtoValidator : AbstractValidator<KycStatusDto>
    {
        public KycStatusDtoValidator()
        {
            RuleFor(x => x.KycStatus)
                .IsInEnum()
                .WithMessage("KycStatus must be a valid enum value.");
        }
    }
}