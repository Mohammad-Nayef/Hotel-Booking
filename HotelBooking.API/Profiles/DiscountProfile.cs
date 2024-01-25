using AutoMapper;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Profiles
{
    internal class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<DiscountCreationDTO, DiscountDTO>();
        }
    }
}
