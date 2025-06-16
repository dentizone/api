using AutoMapper;
using Dentizone.Application.DTOs.Order;
using Dentizone.Application.Interfaces.Order;
using Dentizone.Application.Interfaces.Post;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Infrastructure;

namespace Dentizone.Application.Services
{
    internal class OrderService(
        IMapper mapper,
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository,
        IOrderStatusRepository orderStatusRepository,
        IPostService postService,
        IShipInfoRepository shipInfoRepository,
        AppDbContext dbContext)
        : IOrderService
    {
        public async Task<OrderViewDto?> CancelOrderAsync(string orderId, string userId)
        {
            var order = await orderRepository.FindBy(o => o.Id == orderId, [o => o.OrderStatuses]);

            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            if (order.BuyerId != userId)
            {
                throw new UnauthorizedAccessException("You are not allowed to cancel this order.");
            }

            if (order.OrderStatuses.Any(os => os.Status == OrderStatues.Cancelled))
            {
                throw new BadActionException("Order is already canceled.");
            }

            var orderStatus = new OrderStatus
            {
                OrderId = order.Id,
                Status = OrderStatues.Cancelled,
            };
            await orderStatusRepository.CreateAsync(orderStatus);

            var dto = mapper.Map<OrderViewDto>(order);
            return dto;
        }


        public async Task<string> CreateOrderAsync(CreateOrderDto createOrderDto, string buyerId)
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var posts = await postService.ValidatePosts(createOrderDto.PostIds);

                var order = new Order
                {
                    BuyerId = buyerId,
                    CommissionAmount = 0.2m,
                    TotalAmount = posts.Sum(p => p.Price) * 1.2m,
                };

                var result = await orderRepository.CreateAsync(order);
                if (result == null)
                {
                    throw new BadActionException("Failed Order");
                }

                var orderStatus = new OrderStatus
                {
                    OrderId = result.Id,
                    Status = OrderStatues.Placed,
                };
                await orderStatusRepository.CreateAsync(orderStatus);

                // Create Order Items

                foreach (var post in posts)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = result.Id,
                        PostId = post.Id,
                    };
                    await orderItemRepository.CreateAsync(orderItem);
                }

                // Create Ship Info 

                var shipInfo = new ShipInfo
                {
                    OrderId = result.Id,
                    Street = createOrderDto.ShipInfo.Address,
                    City = createOrderDto.ShipInfo.City,
                    UserId = buyerId
                };
                await shipInfoRepository.CreateAsync(shipInfo);

                // Mark the post as Sold
                foreach (var post in posts)
                {
                    await postService.UpdatePostStatus(post.Id, PostStatus.Sold);
                }


                await transaction.CommitAsync();
                return result.Id;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task<OrderViewDto> GetOrderByIdAsync(string orderId, string buyerId)
        {
            var order = await orderRepository.GetOrderDetails(orderId, buyerId);
            if (order == null)
            {
                throw new Exception("Order not Found");
            }

            return mapper.Map<OrderViewDto>(order);
        }

        public async Task<List<OrderViewDto>> GetOrdersByBuyerAsync(string buyerId)
        {
            var orders = await orderRepository.GetOrdersWithDetails(buyerId);

            return mapper.Map<List<OrderViewDto>>(orders);
        }
    }
}