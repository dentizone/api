using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dentizone.Application.DTOs.Withdrawal
{
    public class WithdrawalRequestDto
    {
        public decimal Amount { get; set; }
        public string WalletId { get; set; }
    }

    public class WithdrawalRequestDtoValidator : AbstractValidator<WithdrawalRequestDto>
    {
        public WithdrawalRequestDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.")
                .NotEmpty().WithMessage("Amount is required.");
            RuleFor(x => x.WalletId)
                .NotEmpty().WithMessage("Wallet ID is required.");
        }
    }
}
