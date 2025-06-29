using AutoMapper;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;
using System.Linq.Expressions;
using Dentizone.Application.DTOs.UserActivity;

namespace Dentizone.Application.Services
{
    public class UserActivityService(
        IUserActivityRepository userActivityRepository,
        IMapper mapper,
        IRequestContextService requestContextService)
        : IUserActivityService
    {
        public async Task<CreatedUserActivityDto> CreateAsync(UserActivities activity,
            DateTime? detectedAt = null, string? userId = null)
        {
            var userActivity = new UserActivity
            {
                FingerprintToken = requestContextService.GetFingerprint(),
                IpAddress = requestContextService.GetIpAddress(),
                UserAgent = requestContextService.GetUserAgent(),
                Device = requestContextService.GetDeviceType(),
                ActivityType = activity,
                UserId = requestContextService.GetUserId() ?? userId,
                DetectedAt = detectedAt ?? DateTime.UtcNow
            };

            var newUserActivity = await userActivityRepository.CreateAsync(userActivity);
            return mapper.Map<CreatedUserActivityDto>(newUserActivity);
        }

        public async Task<UserActivityDto?> GetByIdAsync(string id)
        {
            var userActivity = await userActivityRepository.GetByIdAsync(id);
            if (userActivity == null) throw new NotFoundException("There's no user activity with this id ");
            return mapper.Map<UserActivityDto>(userActivity);
        }

        public async Task<ICollection<UserActivityDto>> GetAllByActivityTypeAndUserIdAsync(
            int page, string userId, UserActivities activityType)
        {
            Expression<Func<UserActivity, bool>> filter = ua => ua.UserId == userId && ua.ActivityType == activityType;
            var filteredActivities = await userActivityRepository.GetAllBy(page, filter);
            if (!filteredActivities.Any())
                throw new NotFoundException($"No activities found for user {userId} with activity type {activityType}");
            var mapped = mapper.Map<ICollection<UserActivityDto>>(filteredActivities);
            return mapped;
        }
    }
}