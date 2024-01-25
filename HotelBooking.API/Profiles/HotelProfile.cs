using AutoMapper;
using HotelBooking.Api.Models.Hotel;
using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Api.Profiles
{
    internal class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<HotelCreationDTO, HotelDTO>();
            CreateMap<HotelDTO, HotelUpdateDTO>()
                .ReverseMap();
        }
    }
}
