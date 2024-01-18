using AutoMapper;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Profiles
{
    internal class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingDTO, BookingTable>();
        }
    }
}
