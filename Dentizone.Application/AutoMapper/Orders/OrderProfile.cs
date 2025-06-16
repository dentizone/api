using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.AutoMapper.Posts;
using Dentizone.Application.DTOs.Order;
using Dentizone.Application.DTOs.PostDTO;
using Dentizone.Domain.Entity;
using StackExchange.Redis;

namespace Dentizone.Application.AutoMapper.Orders
{
    internal class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderViewDTO, Domain.Entity.Order>()
                .ReverseMap();
            CreateMap<CreateOrderDTO, Domain.Entity.Order>()
                .ReverseMap();
        }
    }
}
