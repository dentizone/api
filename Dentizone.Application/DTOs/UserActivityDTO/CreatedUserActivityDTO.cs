using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.UserActivityDTO
{
    public class CreatedUserActivityDto
    {
        public DateTime DetectedAt { get; set; }
        public UserActivities ActivityType { get; set; }
    }

    public class CreatedUserActivityDTOValidator : AbstractValidator<CreatedUserActivityDto>
    {
        public CreatedUserActivityDTOValidator()
        {
            RuleFor(x => x.DetectedAt).NotEmpty().WithMessage("DetectedAt is required.");
            RuleFor(x => x.ActivityType).IsInEnum().WithMessage("ActivityType must be a valid enum value.");
        }
    }
}