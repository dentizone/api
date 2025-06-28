using AutoMapper;
using Dentizone.Application.DTOs;
using Dentizone.Application.DTOs.Order;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Interfaces.Cart;
using Dentizone.Application.Interfaces.Order;
using Dentizone.Application.Interfaces.Post;
using Dentizone.Application.Services.Payment;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Mail;
using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Infrastructure;
using System.Linq.Expressions;

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
        ICartService cartService,
        AppDbContext dbContext)
        : IOrderService
    {
        public async Task<OrderViewDto?> CancelOrderAsync(string orderId, string userId)
        {
            var order = await orderRepository.FindBy(o => o.Id == orderId,
                [o => o.OrderStatuses, o => o.OrderItems]);

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

            if (order.OrderStatuses.Any(os => os.Status == OrderStatues.Arrived))
            {
                throw new BadActionException("Order is already completed, cannot cancel.");
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

            // Get the Payment by order id to cancel it
            await paymentService.CancelPaymentByOrderId(order.Id);


            var dto = mapper.Map<OrderViewDto>(order);
            return dto;
        }

        private async Task SendConfirmationEmail(List<string> sellerEmails, string buyerEmail, string orderId)
        {
            var emailContent = $"Your order with ID {orderId} has been successfully placed.";
            await mailService.Send(buyerEmail, "Order Confirmation", emailContent);

            foreach (var email in sellerEmails)
            {
                await mailService.Send(email, "New Order Placed",
                    $"Your post has been sold. Wait for pickup. Order ID: {orderId}");
            }
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

                // Get Buyer and Seller Emails

                var buyer = await authService.GetById(buyerId);

                // TEMPO SOLUTION UNTIL WE MAKE THE NEW MIGRATIONS
                List<string> sellerEmails = [];
                foreach (var post in posts)
                {
                    var seller = await authService.GetById(post.SellerId);
                    sellerEmails.Add(seller.Email!);
                }

                await SendConfirmationEmail(sellerEmails, buyer.Email, order.Id);
                // Reset Cart

                await cartService.ClearCartAsync(buyerId);

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

        public async Task CompleteOrder(string orderId)
        {
            await paymentService.ConfirmPaymentAsync(orderId);

            // Mark Order as Completed
            var order = await orderRepository.FindBy(o => o.Id == orderId, [o => o.OrderStatuses]);
            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            if (order.OrderStatuses.Any(os => os.Status == OrderStatues.Arrived))
            {
                throw new BadActionException("Order is already completed.");
            }

            if (order.OrderStatuses.Any(os => os.Status == OrderStatues.Cancelled))
            {
                throw new BadActionException("Order is cancelled, cannot complete.");
            }

            var orderStatus = new OrderStatus
            {
                OrderId = order.Id,
                Status = OrderStatues.Arrived,
            };

            await orderStatusRepository.CreateAsync(orderStatus);
        }

        public async Task<PagedResultDto<OrderViewDto>> GetOrders(int page, FilterOrderDto filters)
        {
            // Apply filters if any

            Expression<Func<Order, bool>> filterExpression = o =>
                (string.IsNullOrEmpty(filters.BuyerName) || o.Buyer.FullName.Contains(filters.BuyerName)) &&
                (string.IsNullOrEmpty(filters.OrderId) || o.Id.ToString().Contains(filters.OrderId)) &&
                (filters.Status == null ||
                 o.OrderStatuses.Any(os => os.Status.ToString().Contains(filters.Status.ToString())));


            var order = await orderRepository.GetAllAsync(
                page,
                filterExpression
            );

            return mapper.Map<PagedResultDto<OrderViewDto>>(order);
        }

        public async Task<IEnumerable<Order>> GetReviewedOrdersByUserId(string userId)
        {
            var orders = await orderRepository.GetAllAsync(
                null,
                o => o.IsReviewed && o.OrderItems.Any(oi => oi.Post.SellerId == userId)
            );
            return orders.Items;
        }
    }
}