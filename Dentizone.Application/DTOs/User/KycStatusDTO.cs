using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.User
{
    public class KycStatusDTO
    {
        public KycStatus KycStatus { get; set; }
    }
    public class KycStatusDTOValidator : AbstractValidator<KycStatusDTO>
    {
        public KycStatusDTOValidator()
        {
            RuleFor(x => x.KycStatus)
                .IsInEnum()
                .WithMessage("KycStatus must be a valid enum value.");
        }
    }
}
