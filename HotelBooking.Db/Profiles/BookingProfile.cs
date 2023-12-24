using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Profiles
{
    internal class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingDTO, BookingTable>();
        }
    }
}
