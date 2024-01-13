using AutoMapper;
using HotelBooking.Api.Models.CartItem;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Profiles
{
    internal class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItemCreationDTO, CartItemDTO>();
            CreateMap<CartItemCreationDTO, CartItemCreationResponseDTO>();
        }
    }
}
