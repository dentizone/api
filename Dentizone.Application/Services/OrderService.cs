using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs.Catalog;
using Dentizone.Application.DTOs.Order;
using Dentizone.Application.Interfaces.Order;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Infrastructure.Repositories;
using StackExchange.Redis;
using static StackExchange.Redis.Role;

namespace Dentizone.Application.Services
{
    internal class OrderService:IOrderService
    {
        IMapper _mapper;
        IOrderItemRepository _orderItemRepository;
        IOrderRepository _orderRepository;
        IOrderStatusRepository _orderStatusRepository;

        public OrderService(IMapper mapper, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IOrderStatusRepository orderStatusRepository) 
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _orderStatusRepository = orderStatusRepository;

        }

      
            public async Task<List<OrderViewDTO>> CancelOrderAsync(string orderId, string userId)
        {
            
            Expression<Func<Domain.Entity.Order, bool>> condition = o => o.Id == orderId && !o.IsDeleted;
            Expression<Func<Domain.Entity.Order, object>>[] includes = { o => o.Posts, o => o.OrderItems };

            var order = await _orderRepository.FindBy(condition, includes);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }
            if (order.BuyerId != userId)
            {
                throw new UnauthorizedAccessException("You are not allowed to cancel this order.");
            }
            if (order.OrderStatuses.Any(os => os.Status == Domain.Enums.OrderStatues.Cancelled))
            {
                throw new Exception("Order is already canceled.");
            }

            var orderStatus = new OrderStatus
            {
                OrderId = order.Id,
                Status = Domain.Enums.OrderStatues.Cancelled,

            };
            await _orderStatusRepository.CreateAsync(orderStatus);
    
            var dto = _mapper.Map<OrderViewDTO>(order);
            return new List<OrderViewDTO> { dto };
        }

   
        

   
        public async Task<string> CreateOrderAsync(CreateOrderDTO createOrderDTO, string buyerId)
        {
            var posts = await _orderRepository.GetPostsByIdsAsync(createOrderDTO.PostIds);
            if(posts.Count!= createOrderDTO.PostIds.Count)
            {
                throw new Exception("some post IDs are invalid");
            }

            var order = new Domain.Entity.Order
            {
                BuyerId = buyerId,
                Posts = posts,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _orderRepository.CreateAsync(order);
            if(result==null)
            {
                throw new Exception("Failed Order");
            }

            var orderStatus = new OrderStatus
            {
                OrderId = result.Id,
                Status = Domain.Enums.OrderStatues.Placed,
              
            };
            await _orderStatusRepository.CreateAsync(orderStatus);
            return order.Id;

        }

        public async Task<OrderViewDTO> GetOrderByIdAsync(string orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new Exception("Order not Found");
            }

            return _mapper.Map<OrderViewDTO>(order);
        }

        public async Task<List<OrderViewDTO>> GetOrdersByBuyerAsync(string buyerId, int page)
        {
            Expression<Func<Domain.Entity.Order, bool>> filter = o => o.BuyerId == buyerId && !o.IsDeleted;

            Expression<Func<Domain.Entity.Order, object>>[] includes =
            {
        o => o.Posts,
        o => o.OrderItems,
        o => o.OrderStatuses
    };

            var orders = await _orderRepository.GetAllAsync(page, filter, o => o.CreatedAt, includes);

            return _mapper.Map<List<OrderViewDTO>>(orders);
        }

       
    }
}
