using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Profiles
{
    internal class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<DiscountDTO, DiscountTable>();
        }
    }
}
