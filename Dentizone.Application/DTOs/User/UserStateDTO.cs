using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.User
{
    public class UserStateDTO
    {
        public UserState Status { get; set; }
    }
    public class UserStateDTOValidator : AbstractValidator<UserStateDTO>
    {
        public UserStateDTOValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status must be a valid enum value.");
        }
    }
}
