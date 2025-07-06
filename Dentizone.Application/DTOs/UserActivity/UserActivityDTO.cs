using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.UserActivity
{
    public class UserActivityDto
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public required string UserId { get; set; }
        public required string FingerprintToken { get; set; }
        public required string Device { get; set; }
        public required string UserAgent { get; set; }
        public DateTime DetectedAt { get; set; }
        public required string IpAddress { get; set; }
        public UserActivities ActivityType { get; set; }
    }

    public class UserActivityDtoValidator : AbstractValidator<UserActivityDto>
    {
        public UserActivityDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("FullName is required.");
            RuleFor(x => x.FingerprintToken).NotEmpty().WithMessage("FingerprintToken is required.");
            RuleFor(x => x.Device).NotEmpty().WithMessage("Device is required.");
            RuleFor(x => x.UserAgent).NotEmpty().WithMessage("UserAgent is required.");
            RuleFor(x => x.DetectedAt).NotEmpty().WithMessage("DetectedAt is required.");
            RuleFor(x => x.IpAddress).NotEmpty().WithMessage("IpAddress is required.");
            RuleFor(x => x.ActivityType).IsInEnum().WithMessage("ActivityType must be a valid enum value.");
        }
    }
}