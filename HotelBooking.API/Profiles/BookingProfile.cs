using AutoMapper;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Profiles
{
    internal class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingCreationDTO, BookingDTO>();
        }
    }
}
