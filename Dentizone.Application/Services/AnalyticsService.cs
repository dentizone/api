using Dentizone.Application.DTOs.Analytics;
using Dentizone.Application.Interfaces.Analytics;
using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Infrastructure.Cache;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Dentizone.Application.Services
{
    internal class AnalyticsService(
        IUserRepository userRepository,
        IPostRepository postRepository,
        IRedisService _redisService,
        IOrderRepository orderRepository)
        : IAnalyticsService
    {
        public async Task<PostAnalyticsDto> GetPostAnalyticsAsync(bool useCache = false)
        {
            var cacheKey = CacheHelper.GenerateCacheKey("analytics", "post");

            if (useCache)
            {
                var cachedValue = await _redisService.GetValue(cacheKey);
                if (cachedValue != null)
                {
                    return JsonConvert.DeserializeObject<PostAnalyticsDto>(cachedValue)!;
                }
            }


            var numberOfPosts = await postRepository.GetActivePosts().CountAsync();
            var averageValueOfOrders = await postRepository.AveragePostsPriceAsync();
            var postsByCategory = await postRepository.GetPostCountPerCategoryAsync();
            var returnedDto = new PostAnalyticsDto
                              {
                                  TotalPosts = numberOfPosts,
                                  AveragePostPrice = averageValueOfOrders,
                                  PostsByCategory = postsByCategory
                              };
            //hyupdate lma ygeb mn el DB 
            
                var serialized = JsonConvert.SerializeObject(returnedDto);
                await _redisService.SetValue(cacheKey, serialized, TimeSpan.FromMinutes(1800));
           

            return returnedDto;
        }

       

        public async Task<SalesAnalyticsDto> GetSalesAnalyticsAsync(bool useCache = false)
        {
            var cacheKey = CacheHelper.GenerateCacheKey("analytics", "Sales");

            if (useCache)
            {
                var cachedValue = await _redisService.GetValue(cacheKey);
                if (cachedValue != null)
                {
                    return JsonConvert.DeserializeObject<SalesAnalyticsDto>(cachedValue)!;
                }
            }
            var numberOfOrders = await orderRepository.CountTotalOrders();
            var averageValueOfOrders = await orderRepository.AverageValueOfOrders();
            var returnedDto = new SalesAnalyticsDto
                              {
                                  TotalsOrder = numberOfOrders,
                                  AveragePostPrice = averageValueOfOrders,
                              };
            
                var serialized = JsonConvert.SerializeObject(returnedDto);
                await _redisService.SetValue(cacheKey, serialized, TimeSpan.FromMinutes(1800));
            
            return returnedDto;
        }

        public async Task<UserAnalyticsDto> GetUserAnalyticsAsync( bool useCache = false)
        {
            var cacheKey = CacheHelper.GenerateCacheKey("analytics", "Sales");

            if (useCache)
            {
                var cachedValue = await _redisService.GetValue(cacheKey);
                if (cachedValue != null)
                {
                    return JsonConvert.DeserializeObject<UserAnalyticsDto>(cachedValue)!;
                }
            }
            var allUsers = await userRepository.GetCountOfUsersAsync();
            var allUsersLast7Days = await userRepository.GetCount7DaysAsync();
            var allUsersLast30Days = await userRepository.GetCount30DaysAsync();
            var allUsersPerUniversity = await userRepository.GetStudentCountPerUniversityAsync();
            var returnedDto = new UserAnalyticsDto
                              {
                                  TotalUsers = allUsers,
                                  NewUsersLast7Days = allUsersLast7Days,
                                  NewUsersLast30Days = allUsersLast30Days,
                                  UsersByUniversity = allUsersPerUniversity
                              };
            
                var serialized = JsonConvert.SerializeObject(returnedDto);
                await _redisService.SetValue(cacheKey, serialized, TimeSpan.FromMinutes(1800));
            
            return returnedDto;
        }
    }
}