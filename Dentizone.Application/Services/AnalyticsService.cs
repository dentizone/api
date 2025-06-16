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

        public Task<PostAnalyticsDTO> GetPostAnalyticsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SalesAnalyticsDTO> GetSalesAnalyticsAsync()
        {
            throw new NotImplementedException();
        }

        public  async Task<UserAnalyticsDTO> GetUserAnalyticsAsync()
        {
            var AllUsers=await _userRepository.GetCountOfUsersAsync();
            var allUsersLast7Days = await _userRepository.GetCount7DaysAsync();
            var allUsersLast30Days = await _userRepository.GetCount30DaysAsync();
            var returnedDTO=new UserAnalyticsDTO
            {
                TotalUsers = AllUsers,
                NewUsersLast7Days = allUsersLast7Days,
                NewUsersLast30Days = allUsersLast30Days
            };
            return returnedDTO;

        }

    }
}
