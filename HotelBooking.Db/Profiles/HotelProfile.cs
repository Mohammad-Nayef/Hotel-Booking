using AutoMapper;
using HotelBooking.Db.Extensions;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Profiles
{
    internal class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<HotelDTO, HotelTable>()
                .ReverseMap();

            CreateMap<HotelTable, FeaturedHotelDTO>()
                .ForMember(dest => dest.ThumbnailId, opt =>
                    opt.MapFrom(src => src.Images.FirstOrDefault().Id))
                .ForMember(dest => dest.CurrentDiscount, opt =>
                    opt.MapFrom(src => src.Discounts.GetHighestActive()))
                .ForMember(dest => dest.CityName, opt =>
                    opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.CountryName, opt =>
                    opt.MapFrom(src => src.City.CountryName));

            CreateMap<HotelTable, HotelForUserDTO>()
                .ForMember(dest => dest.ThumbnailId, opt =>
                    opt.MapFrom(src => src.Images.FirstOrDefault().Id));

            CreateMap<HotelTable, HotelPageDTO>()
                .ForMember(dest => dest.ImagesIds, opt =>
                    opt.MapFrom(src => src.Images.Select(image => image.Id)))
                .ForMember(dest => dest.CurrentDiscount, opt =>
                    opt.MapFrom(src => src.Discounts.GetHighestActive()));
        }
    }
}
