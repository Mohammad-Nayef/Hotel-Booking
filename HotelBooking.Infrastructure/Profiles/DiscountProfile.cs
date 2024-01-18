using AutoMapper;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Profiles
{
    internal class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<DiscountDTO, DiscountTable>()
                .ReverseMap();
        }
    }
}
