using AutoMapper;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Profiles
{
    internal class HotelVisitProfile : Profile
    {
        public HotelVisitProfile()
        {
            CreateMap<HotelVisitDTO, HotelVisitTable>();
        }
    }
}
