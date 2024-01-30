using AutoMapper;
using HotelBooking.Domain.Models.City;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Profiles
{
    internal class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CityDTO, CityTable>()
                .ReverseMap();
            CreateMap<CityTable, CityForUserDTO>();
            CreateMap<CityTable, PopularCityDTO>();
        }
    }
}
