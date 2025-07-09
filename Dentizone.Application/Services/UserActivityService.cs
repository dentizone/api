using AutoMapper;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;
using System.Linq.Expressions;
using Dentizone.Application.DTOs;
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
                FingerprintToken = "NON",
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

        public async Task<PagedResultDto<ActivityView>> GetAll(ActivityFilterDto filters)
        {
            Expression<Func<UserActivity, bool>> filter = a =>
                (!filters.ActivityType.HasValue || a.ActivityType == filters.ActivityType.Value) &&
                (string.IsNullOrEmpty(filters.UserId) || a.UserId == filters.UserId) &&
                (string.IsNullOrEmpty(filters.Device) || a.Device.Contains(filters.Device)) &&
                (string.IsNullOrEmpty(filters.IpAddress) || a.IpAddress.Contains(filters.IpAddress)) &&
                (!filters.DetectedAfter.HasValue || a.DetectedAt >= filters.DetectedAfter.Value) &&
                (!filters.DetectedBefore.HasValue || a.DetectedAt <= filters.DetectedBefore.Value) &&
                (string.IsNullOrEmpty(filters.SearchText) ||
                 a.Device.ToLower().Contains(filters.SearchText.ToLower()) ||
                 a.UserAgent.ToLower().Contains(filters.SearchText.ToLower()) ||
                 a.IpAddress.ToLower().Contains(filters.SearchText.ToLower()));


            var userActivities = await userActivityRepository.GetAllAsync(filters.PageNumber, filter);


            return mapper.Map<PagedResultDto<ActivityView>>(userActivities);
        }
    }
}