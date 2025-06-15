using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs.Cart;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper.Carts
{
    internal class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartItemDTO, Cart>().ReverseMap();
        }
    }
}
