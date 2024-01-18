using AutoMapper;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Profiles
{
    internal class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItemDTO, CartItemTable>()
                .ReverseMap();
        }
    }
}
