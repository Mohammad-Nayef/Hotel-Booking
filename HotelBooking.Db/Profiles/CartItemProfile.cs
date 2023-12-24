using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Profiles
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
