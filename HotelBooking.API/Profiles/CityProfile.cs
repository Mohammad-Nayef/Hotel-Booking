using AutoMapper;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Profiles
{
    internal class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CityCreationDTO, CityDTO>();
            CreateMap<CityCreationDTO, CityCreationResponseDTO>();
            CreateMap<CityDTO, CityUpdateDTO>();
            CreateMap<CityUpdateDTO, CityDTO>();
        }
    }
}
