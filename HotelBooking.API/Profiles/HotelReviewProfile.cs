using AutoMapper;
using HotelBooking.Api.Models.Hotel;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Profiles
{
    internal class HotelReviewProfile : Profile
    {
        public HotelReviewProfile()
        {
            CreateMap<HotelReviewCreationDTO, HotelReviewDTO>();
        }
    }
}
