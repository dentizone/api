using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.UserActivity
{
    public class CreatedUserActivityDto
    {
        public DateTime DetectedAt { get; set; }
        public UserActivities ActivityType { get; set; }
    }

    public class CreatedUserActivityDtoValidator : AbstractValidator<CreatedUserActivityDto>
    {
        public CreatedUserActivityDtoValidator()
        {
            RuleFor(x => x.DetectedAt).NotEmpty().WithMessage("DetectedAt is required.");
            RuleFor(x => x.ActivityType).IsInEnum().WithMessage("ActivityType must be a valid enum value.");
        }
    }
}