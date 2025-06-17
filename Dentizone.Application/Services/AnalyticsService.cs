using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs.Analytics;
using Dentizone.Application.Interfaces.Analytics;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Application.Services
{
    internal class AnalyticsService : IAnalyticsService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IOrderRepository _orderRepository;
        public AnalyticsService(IUserRepository userRepository, IPostRepository postRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _orderRepository = orderRepository;
        }

        public async Task<PostAnalyticsDTO> GetPostAnalyticsAsync()
        {
            var numberOfPosts = await _postRepository.TotalNumberPostsAsync();
            var AverageValueOfOrders = await _postRepository.AveragePostsPriceAsync();
            var countofpostsperCategory = await _postRepository.GetPostCountPerCategoryAsync();
            var returnedDTO = new PostAnalyticsDTO
            {
                TotalPosts = numberOfPosts,
                AveragePostPrice =AverageValueOfOrders,
                PostsByCategory = countofpostsperCategory

            };
            return returnedDTO;
        }

        public async Task<SalesAnalyticsDTO> GetSalesAnalyticsAsync()
        {
            var numberOfOrders = await _orderRepository.CountTotalOrders();
            var AverageValueOfOrders = await _orderRepository.AverageValueOfOrders();
            var returnedDTO = new SalesAnalyticsDTO
            {
                TotalsOrder = numberOfOrders,
                AveragePostPrice = (int)AverageValueOfOrders,
            };
            return returnedDTO;
        }

        public  async Task<UserAnalyticsDTO> GetUserAnalyticsAsync()
        {
            var AllUsers=await _userRepository.GetCountOfUsersAsync();
            var allUsersLast7Days = await _userRepository.GetCount7DaysAsync();
            var allUsersLast30Days = await _userRepository.GetCount30DaysAsync();
            var allUsersPerUniversity = await _userRepository.GetStudentCountPerUniversityAsync();
            var returnedDTO=new UserAnalyticsDTO
            {
                TotalUsers = AllUsers,
                NewUsersLast7Days = allUsersLast7Days,
                NewUsersLast30Days = allUsersLast30Days,
                UsersByUniversity = allUsersPerUniversity
            };
            return returnedDTO;

        }
        


    }
}
