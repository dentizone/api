using AutoMapper;
using Dentizone.Application.DTOs;
using Dentizone.Application.DTOs.Order;
using Dentizone.Application.DTOs.Shipping;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Application.AutoMapper;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderStatus, OrderStatusTimeline>()
            .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.CreatedAt));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post.Title))
            .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Post.Id))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Post.Price));

        CreateMap<ShipInfo, OrderShipInfoDto>();

        CreateMap<Order, OrderViewDto>()
            .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(src => src.Buyer.FullName))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.StatusTimeline, opt => opt.MapFrom(src => src.OrderStatuses))
            .ForMember(dest => dest.OrderShipmentAddress, opt => opt.MapFrom(src => src.ShipInfo));
        CreateMap<Order, OrderViewAll>()
            .IncludeBase<Order, OrderViewDto>()
            .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Buyer.Id))
            .ForMember(dest => dest.Sellers, opt => opt.MapFrom(src => src.OrderItems
                                                                          .Select(oi => oi.Post.Seller)
                                                                          .Distinct()
                                                                          .Select(s => new SellerInfo
                                                                          {
                                                                              SellerId = s.Id,
                                                                              SellerName = s.FullName,
                                                                              SellerEmail = s.Email
                                                                          })))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems
                                                                             .Select(oi => new OrderItemWithPickup
                                                                             {
                                                                                 Id = oi.Id,
                                                                                 PostId = oi.PostId,
                                                                                 PostTitle = oi.Post.Title,
                                                                                 Price = oi.Post.Price,
                                                                                 PickupLocation =
                                                                                         $"{oi.Post.Street} - {oi.Post.City}"
                                                                             })))
            .ForMember(dest => dest.ShipmentStatus, opt => opt.MapFrom(src => src.OrderItems
                                                                           .SelectMany(oi => oi.ShipmentActivities
                                                                               .Select(sa => new ShipView
                                                                               {
                                                                                   id = sa.Id,
                                                                                   ShipmentActivityStatus =
                                                                                           sa.Status,
                                                                                   Timestamp = sa
                                                                                           .CreatedAt,
                                                                                   Comment = sa
                                                                                           .ActivityDescription,
                                                                                   ItemName = oi.Post
                                                                                           .Title,
                                                                               }))
                                                                           .DistinctBy(sa => sa.id)
                                                                           .OrderByDescending(sa => sa.Timestamp)));


        CreateMap(typeof(PagedResult<>), typeof(PagedResultDto<>))
            .ForMember("Items", opt => opt.MapFrom("Items"))
            .ForMember("Page", opt => opt.MapFrom("Page"))
            .ForMember("PageSize", opt => opt.MapFrom("PageSize"))
            .ForMember("TotalCount", opt => opt.MapFrom("TotalCount"));
    }
}