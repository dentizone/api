using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.UserActivityDTO
{
    public class CreatedUserActivityDTO
    {
        public string UserId { get; set; }
        public string FingerprintToken { get; set; }
        public string Device { get; set; }
        public string UserAgent { get; set; }
        public DateTime DetectedAt { get; set; }
        public string IpAddress { get; set; }
        public UserActivities ActivityType { get; set; }
    }
    public class CreatedUserActivityDTOValidator : AbstractValidator<CreatedUserActivityDTO>
    {
        public CreatedUserActivityDTOValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.FingerprintToken).NotEmpty().WithMessage("FingerprintToken is required.");
            RuleFor(x => x.Device).NotEmpty().WithMessage("Device is required.");
            RuleFor(x => x.UserAgent).NotEmpty().WithMessage("UserAgent is required.");
            RuleFor(x => x.DetectedAt).NotEmpty().WithMessage("DetectedAt is required.");
            RuleFor(x => x.IpAddress).NotEmpty().WithMessage("IpAddress is required.");
            RuleFor(x => x.ActivityType).IsInEnum().WithMessage("ActivityType must be a valid enum value.");
        }
    }
}
