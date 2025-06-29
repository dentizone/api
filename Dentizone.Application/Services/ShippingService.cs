using Dentizone.Application.Interfaces.Order;
using Dentizone.Application.Services.Authentication;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Mail;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Application.Services
{
    internal class ShippingService(
        IOrderItemRepository orderItemRepository,
        IShipmentActivityRepository shipmentActivityRepository,
        IMailService mailService,
        AuthService authService)
        : IShippingService
    {
        public async Task UpdateItemShipmentStatusAsync(string orderItemId, ShipmentActivityStatus newStatus,
            string? comments)
        {
            //search in DataBase if orderItemId found or not?

            var item = await orderItemRepository.FindBy(
                oi => oi.Id == orderItemId,
                [oi => oi.ShipmentActivities]);

            if (item == null)
            {
                throw new NotFoundException("Not Found");
            }
            else
            {
                var shipmentActivity = new ShipmentActivity
                {
                    ItemId = orderItemId,
                    Status = newStatus,
                    ActivityDescription = comments
                };
                await shipmentActivityRepository.CreateAsync(shipmentActivity);


                var seller = await authService.GetById(item.Post.SellerId);


                await mailService.Send(seller.Email, $"the Status has been changed to {newStatus}",
                    "New status update");

                var buyer = await authService.GetById(item.Order.BuyerId);

                await mailService.Send(buyer.Email, $"the Status has been changed to {newStatus}",
                    "New status update");
            }
        }
    }
}