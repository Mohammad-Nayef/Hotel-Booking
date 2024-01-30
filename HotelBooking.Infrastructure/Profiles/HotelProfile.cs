using AutoMapper;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Infrastructure.Extensions;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Profiles
{
    internal class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<HotelDTO, HotelTable>()
                .ReverseMap();

            CreateMap<HotelTable, FeaturedHotelDTO>()
                .ForMember(dest => dest.CurrentDiscount, opt =>
                    opt.MapFrom(src => src.Discounts.GetHighestActive()))
                .ForMember(dest => dest.CityName, opt =>
                    opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.CountryName, opt =>
                    opt.MapFrom(src => src.City.CountryName));

            CreateMap<HotelTable, HotelForUserDTO>();

            CreateMap<HotelTable, HotelPageDTO>()
                .ForMember(dest => dest.CurrentDiscount, opt =>
                    opt.MapFrom(src => src.Discounts.GetHighestActive()));

            CreateMap<HotelTable, VisitedHotelDTO>();
        }
    }
}
