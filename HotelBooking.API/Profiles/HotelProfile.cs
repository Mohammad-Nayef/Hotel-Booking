using AutoMapper;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Profiles
{
    internal class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<HotelCreationDTO, HotelDTO>();
            CreateMap<HotelCreationDTO, HotelCreationResponseDTO>();
            CreateMap<HotelDTO, HotelUpdateDTO>()
                .ReverseMap();
        }
    }
}
