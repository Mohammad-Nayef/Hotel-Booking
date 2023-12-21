using AutoMapper;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Profiles
{
    internal class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<HotelCreationDTO, HotelDTO>();
            CreateMap<HotelCreationDTO, HotelForCreationResponseDTO>();
        }
    }
}
