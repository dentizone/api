using Dentizone.Application.DTOs.Analytics;
using Dentizone.Application.Interfaces;
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
        IRedisService redisService,
        IOrderRepository orderRepository)
        : IAnalyticsService
    {
        public async Task<PostAnalyticsDto> GetPostAnalyticsAsync(bool useCache = false)
        {
            var cacheKey = CacheHelper.GenerateCacheKey("analytics", "post");

            if (useCache)
            {
                var cachedValue = await redisService.GetValue(cacheKey);
                if (cachedValue != null)
                {
                    return JsonConvert.DeserializeObject<PostAnalyticsDto>(cachedValue)!;
                }
            }


            var total = await postRepository.GetTotalPosts().CountAsync();

            var numberOfPosts = await postRepository.GetActivePosts().CountAsync();
            var pendingPosts = await postRepository.GetPendingPosts().CountAsync();
            var averageValueOfOrders = await postRepository.AveragePostsPriceAsync();
            var postsByCategory = await postRepository.GetPostCountPerCategoryAsync();
            var returnedDto = new PostAnalyticsDto
            {
                PendingPosts = pendingPosts,
                TotalPosts = total,
                ActivePosts = numberOfPosts,
                AveragePostPrice = averageValueOfOrders,
                PostsByCategory = postsByCategory
            };

            var serialized = JsonConvert.SerializeObject(returnedDto);
            await redisService.SetValue(cacheKey, serialized, TimeSpan.FromMinutes(1));


            return returnedDto;
        }


        public async Task<SalesAnalyticsDto> GetSalesAnalyticsAsync(bool useCache = false)
        {
            var cacheKey = CacheHelper.GenerateCacheKey("analytics", "Sales");

            if (useCache)
            {
                var cachedValue = await redisService.GetValue(cacheKey);
                if (cachedValue != null)
                {
                    return JsonConvert.DeserializeObject<SalesAnalyticsDto>(cachedValue)!;
                }
            }

            var numberOfOrders = await orderRepository.CountTotalOrders();
            var averageValueOfOrders = await orderRepository.AverageValueOfOrders();
            var totalRevenue = await orderRepository.TotalRevenue();
            var returnedDto = new SalesAnalyticsDto
            {
                TotalSalesRevenue = totalRevenue,
                TotalsOrder = numberOfOrders,
                AveragePostPrice = averageValueOfOrders,
            };

            var serialized = JsonConvert.SerializeObject(returnedDto);
            await redisService.SetValue(cacheKey, serialized, TimeSpan.FromMinutes(1));

            return returnedDto;
        }

        public async Task<UserAnalyticsDto> GetUserAnalyticsAsync(bool useCache = false)
        {
            var cacheKey = CacheHelper.GenerateCacheKey("analytics", "User");

            if (useCache)
            {
                var cachedValue = await redisService.GetValue(cacheKey);
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
                UsersByUniversity = allUsersPerUniversity,
                PendingKyc = await userRepository.GetPendingKycCount()
            };

            var serialized = JsonConvert.SerializeObject(returnedDto);
            await redisService.SetValue(cacheKey, serialized, TimeSpan.FromMinutes(1));

            return returnedDto;
        }
    }
}