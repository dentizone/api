using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Interfaces.Order;
using Dentizone.Application.Services.Authentication;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces.Mail;
using Dentizone.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Hosting;

namespace Dentizone.Application.Services { 
    internal class ShippingService : IShippingService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IShipmentActivityRepository _shipmentActivityRepository;
    private readonly IMailService _mailService;
        private readonly AuthService _authService;

        public ShippingService(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IShipmentActivityRepository shipmentActivityRepository, IMailService mailService, AuthService authService)
        {
            _mailService = mailService;
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _shipmentActivityRepository = shipmentActivityRepository;
            _authService = authService;
        }
        public async void UpdateItemShipmentStatusAsync(string orderItemId, ShipmentActivityStatus newStatus)
        {
            //search in DataBase if orderItemId found or not?
           
            OrderItem? item = await _orderItemRepository.FindBy(
            oi => oi.Id == orderItemId,
            new Expression<Func<OrderItem, object>>[] { oi => oi.ShipmentActivities });

            if (item == null) {
                throw new Exception("Not Found");
            }
            else
            {
                ShipmentActivity shipmentActivity = new ShipmentActivity();
                shipmentActivity.Id = orderItemId;
                shipmentActivity.Status = newStatus;
                _shipmentActivityRepository.CreateAsync(shipmentActivity);

                
                var seller = await _authService.GetById(item.Post.SellerId);
                
                

                await _mailService.Send(seller.Email, $"the Status has been changed to {newStatus}",
                "New status update");

                var buyer = await _authService.GetById(item.Order.BuyerId);

                await _mailService.Send(buyer.Email, $"the Status has been changed to {newStatus}",
                "New status update");

            }


        }
    }
}
