using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Profiles
{
    internal class HotelReviewProfile : Profile
    {
        public HotelReviewProfile()
        {
            CreateMap<HotelReviewDTO, HotelReviewTable>()
                .ReverseMap();
        }
    }
}
