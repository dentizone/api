using Dentizone.Application.DTOs.UserActivity;
using Dentizone.Domain.Enums;

namespace Dentizone.Application.Interfaces;

public interface IUserActivityService
{
    Task<CreatedUserActivityDto> CreateAsync(UserActivities activity,
        DateTime? detectedAt = null, string? userId = null);

    Task<UserActivityDto?> GetByIdAsync(string id);

    Task<ICollection<UserActivityDto>> GetAllByActivityTypeAndUserIdAsync(
        int page, string userId, UserActivities activityType);
}