using Dentizone.Application.DTOs.UserActivityDTO;
using Dentizone.Domain.Enums;

namespace Dentizone.Application.Interfaces;

public interface IUserActivityService
{
    Task<CreatedUserActivityDto> CreateAsync(UserActivities activity,
                                             DateTime? detectedAt = null, string? userId = null);

    Task<UserActivityDTO?> GetByIdAsync(string id);

    Task<ICollection<UserActivityDTO>> GetAllByActivityTypeAndUserIdAsync(
        int page, string userId, UserActivities activityType);
}