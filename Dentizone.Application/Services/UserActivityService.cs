using AutoMapper;
using Dentizone.Application.DTOs.UserActivityDTO;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Dentizone.Application.Services
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IMapper _mapper;
        private readonly IRequestContextService _requestContextService;

        public UserActivityService(IUserActivityRepository userActivityRepository, IMapper mapper,
                                   IRequestContextService requestContextService)
        {
            _userActivityRepository = userActivityRepository;
            _mapper = mapper;
            this._requestContextService = requestContextService;
        }

        public async Task<CreatedUserActivityDto> CreateAsync(UserActivities activity,
                                                              DateTime? detectedAt = null, string? userId = null)
        {
            var userActivity = new UserActivity
                               {
                                   FingerprintToken = _requestContextService.GetFingerprint(),
                                   IpAddress = _requestContextService.GetIpAddress(),
                                   UserAgent = _requestContextService.GetUserAgent(),
                                   Device = _requestContextService.GetDeviceType(),
                                   ActivityType = activity,
                                   UserId = _requestContextService.GetUserId() ?? userId,
                                   DetectedAt = detectedAt ?? DateTime.UtcNow
                               };

            var newUserActivity = await _userActivityRepository.CreateAsync(userActivity);
            return _mapper.Map<CreatedUserActivityDto>(newUserActivity);
        }

        public async Task<UserActivityDTO?> GetByIdAsync(string id)
        {
            var userActivity = await _userActivityRepository.GetByIdAsync(id);
            if (userActivity == null) throw new NotFoundException("There's no user activity with this id ");
            return _mapper.Map<UserActivityDTO>(userActivity);
        }

        public async Task<ICollection<UserActivityDTO>> GetAllByActivityTypeAndUserIdAsync(
            int page, string userId, UserActivities activityType)
        {
            Expression<Func<UserActivity, bool>> filter = ua => ua.UserId == userId && ua.ActivityType == activityType;
            var filteredActivities = await _userActivityRepository.GetAllBy(page, filter);
            if (!filteredActivities.Any())
                throw new NotFoundException($"No activities found for user {userId} with activity type {activityType}");
            var mapped = _mapper.Map<ICollection<UserActivityDTO>>(filteredActivities);
            return mapped;
        }
    }
}