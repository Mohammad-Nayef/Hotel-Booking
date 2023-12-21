using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Profiles
{
    internal class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<HotelDTO, HotelTable>();
        }
    }
}
