using FluentValidation;

namespace Dentizone.Application.Validators
{
    public static class GuidValidator
    {
        public static IRuleBuilderOptions<T, string> MustBeParsableGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(value => Guid.TryParse(value, out var g) && g != Guid.Empty)
                .WithMessage("Invalid or empty GUID.");
        }
    }
}