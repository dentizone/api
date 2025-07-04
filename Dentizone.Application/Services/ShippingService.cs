using Dentizone.Application.DTOs.Shipping;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Mail;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Application.Services
{
    internal class ShippingService(
        IOrderItemRepository orderItemRepository,
        IShipmentActivityRepository shipmentActivityRepository,
        IMailService mailService
    )
        : IShippingService
    {
        public async Task UpdateItemShipmentStatusAsync(CreateShipmentStatusDto shipmentStatus)
        {
            //search in DataBase if orderItemId found or not?

            var item = await orderItemRepository.FindBy(
                oi => oi.Id == shipmentStatus.OrderItemId,
                [oi => oi.ShipmentActivities, oi => oi.Post.Seller, oi => oi.Order.Buyer]);

            if (item == null)
            {
                throw new NotFoundException("Not Found");
            }
            else
            {
                var shipmentActivity = new ShipmentActivity
                {
                    ItemId = shipmentStatus.OrderItemId,
                    Status = shipmentStatus.NewStatus,
                    ActivityDescription = shipmentStatus.Comment ?? "No comment provided",
                };
                await shipmentActivityRepository.CreateAsync(shipmentActivity);


                await mailService.Send(item.Post.Seller.Email,
                    $"the Status has been changed to {shipmentStatus.NewStatus}",
                    "New status update");


                await mailService.Send(item.Order.Buyer.Email,
                    $"the Status has been changed to {shipmentStatus.NewStatus}",
                    "New status update");
            }
        }
    }
}