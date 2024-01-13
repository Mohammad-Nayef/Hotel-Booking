using AutoMapper;
using HotelBooking.Api.Models.City;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Profiles
{
    internal class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CityCreationDTO, CityDTO>();
            CreateMap<CityCreationDTO, CityCreationResponseDTO>();
            CreateMap<CityDTO, CityUpdateDTO>()
                .ReverseMap();
        }
    }
}
