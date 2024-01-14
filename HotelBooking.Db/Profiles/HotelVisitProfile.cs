using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Profiles
{
    internal class HotelVisitProfile : Profile
    {
        public HotelVisitProfile()
        {
            CreateMap<HotelVisitDTO, HotelVisitTable>();
        }
    }
}
