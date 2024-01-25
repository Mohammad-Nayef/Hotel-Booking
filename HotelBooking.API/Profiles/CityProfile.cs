using AutoMapper;
using HotelBooking.Api.Models.City;
using HotelBooking.Domain.Models.City;

namespace HotelBooking.Api.Profiles
{
    internal class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CityCreationDTO, CityDTO>();
            CreateMap<CityDTO, CityUpdateDTO>()
                .ReverseMap();
        }
    }
}
