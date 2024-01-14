using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models.City;

namespace HotelBooking.Db.Profiles
{
    internal class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CityDTO, CityTable>()
                .ReverseMap();

            CreateMap<CityTable, CityForHotelPageDTO>();
        }
    }
}
