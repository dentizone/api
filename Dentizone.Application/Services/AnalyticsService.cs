using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs.Analytics;
using Dentizone.Application.Interfaces.Analytics;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dentizone.Application.Services
{
    internal class AnalyticsService(
        IUserRepository userRepository,
        IPostRepository postRepository,
        IOrderRepository orderRepository)
        : IAnalyticsService
    {
        public async Task<PostAnalyticsDto> GetPostAnalyticsAsync()
        {
            var numberOfPosts = await postRepository.GetActivePosts().CountAsync();
            var averageValueOfOrders = await postRepository.AveragePostsPriceAsync();
            var postsByCategory = await postRepository.GetPostCountPerCategoryAsync();
            var returnedDto = new PostAnalyticsDto
            {
                TotalPosts = numberOfPosts,
                AveragePostPrice = averageValueOfOrders,
                PostsByCategory = postsByCategory
            };
            return returnedDto;
        }

        public async Task<SalesAnalyticsDto> GetSalesAnalyticsAsync()
        {
            var numberOfOrders = await orderRepository.CountTotalOrders();
            var averageValueOfOrders = await orderRepository.AverageValueOfOrders();
            var returnedDto = new SalesAnalyticsDto
            {
                TotalsOrder = numberOfOrders,
                AveragePostPrice = (int)averageValueOfOrders,
            };
            return returnedDto;
        }

        public async Task<UserAnalyticsDto> GetUserAnalyticsAsync()
        {
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
            return returnedDto;
        }
    }
}