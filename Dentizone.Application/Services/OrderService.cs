using AutoMapper;
using Dentizone.Application.DTOs.Order;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Interfaces.Order;
using Dentizone.Application.Interfaces.Post;
using Dentizone.Application.Services.Payment;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Mail;
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
        IMailService mailService,
        IAuthService authService,
        IPaymentService paymentService,
        AppDbContext dbContext)
        : IOrderService
    {
        public async Task<OrderViewDto?> CancelOrderAsync(string orderId, string userId)
        {
            var order = await orderRepository.FindBy(o => o.Id == orderId, [o => o.OrderStatuses, o => o.OrderItems]);

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

            // send cancellation email
            var emailContent = $"Your order with ID {order.Id} has been cancelled.";

            // Get Buyer and Seller Emails

            var buyer = await authService.GetById(order.BuyerId);
            await mailService.Send(buyer.Email, "Order Cancellation", emailContent);

            foreach (var orderItem in order.OrderItems)
            {
                var post = await postService.GetPostById(orderItem.PostId);
                if (post is not null)
                {
                    await postService.UpdatePostStatus(post.Id, PostStatus.Active);
                    var seller = await authService.GetById(post.Seller.Id);
                    await mailService.Send(seller.Email, "Order Cancelled",
                                           $"Your post {post.Title} has been cancelled by the buyer. we relisted it now for sale again@!");
                }
            }


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

                // Create Order Payment
                var paymentDto = new PaymentDto
                {
                    OrderId = result.Id,
                    BuyerId = buyerId,
                    Amount = result.TotalAmount,
                    PaymentMethod = PaymentMethod.COD
                };

                var payment = await paymentService.CreatePaymentAsync(paymentDto);

                // Create Order Items

                foreach (var post in posts)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = result.Id,
                        PostId = post.Id,
                    };
                    await orderItemRepository.CreateAsync(orderItem);
                    // Create a Sale Transaction for each order item
                    await paymentService.CreateSaleTransaction(
                                                               payment.Id, post.Seller.Wallet.Id, post.Price);
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

                // Send Confirmation Email
                var emailContent = $"Your order with ID {result.Id} has been successfully placed.";

                // Get Buyer and Seller Emails

                var buyer = await authService.GetById(buyerId);
                await mailService.Send(buyer.Email, "Order Confirmation", emailContent);

                foreach (var post in posts)
                {
                    var seller = await authService.GetById(post.SellerId);
                    await mailService.Send(seller.Email, "New Order Placed",
                                           $"Your post {post.Title} has been sold. Wait for pickup");
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
                throw new NotFoundException("Order not Found");
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