using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs.CatalogDTOs;
using Dentizone.Application.DTOs.UserActivityDTO;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Repositories;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;

namespace Dentizone.Application.Services
{
    public class UserActivityService
    {
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IMapper _mapper;
        public UserActivityService(IUserActivityRepository userActivityRepository, IMapper mapper)
        {
            _userActivityRepository = userActivityRepository;
            _mapper = mapper;
        }
        public async Task<CreatedUserActivityDTO> CreateAsync(CreatedUserActivityDTO createdUserActivityDTO)
        {
            var userActivity = _mapper.Map<UserActivity>(createdUserActivityDTO);
            var newUserActivity = await _userActivityRepository.CreateAsync(userActivity);
            return _mapper.Map<CreatedUserActivityDTO>(newUserActivity);
        }
        public async Task<UserActivityDTO?> GetByIdAsync(string id)
        {
            var userActivity = await _userActivityRepository.GetByIdAsync(id);
            if (userActivity == null) return null;
            return _mapper.Map<UserActivityDTO>(userActivity);
        }
        public async Task<ICollection<UserActivityDTO>> GetAllByActivityTypeAndUserIdAsync(int page, string userId, UserActivities activityType)
        {      
            Expression<Func<UserActivity, bool>> filter = ua => ua.UserId == userId && ua.ActivityType == activityType;
            var filteredActivities = await _userActivityRepository.GetAllBy(page, filter); 
            var mapped = _mapper.Map<ICollection<UserActivityDTO>>(filteredActivities);
            return mapped;
        }
    }
}
