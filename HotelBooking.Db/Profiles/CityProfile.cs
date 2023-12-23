using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Profiles
{
    internal class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CityDTO, CityTable>()
                .ReverseMap();
        }
    }
}
