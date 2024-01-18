using AutoMapper;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Profiles
{
    internal class HotelReviewProfile : Profile
    {
        public HotelReviewProfile()
        {
            CreateMap<HotelReviewDTO, HotelReviewTable>()
                .ReverseMap();

            CreateMap<HotelReviewTable, ReviewForHotelPageDTO>();
        }
    }
}
