using AutoMapper;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Profiles
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItemCreationDTO, CartItemDTO>();
            CreateMap<CartItemCreationDTO, CartItemCreationResponseDTO>();
        }
    }
}
