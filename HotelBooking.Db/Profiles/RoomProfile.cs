using AutoMapper;
using HotelBooking.Db.Extensions;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Db.Profiles
{
    internal class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomDTO, RoomTable>()
                .ReverseMap();

            CreateMap<RoomTable, RoomForUserDTO>()
                .ForMember(dest => dest.CurrentDiscount, opt =>
                    opt.MapFrom(src => src.Hotel.Discounts.GetHighestActive()))
                .ForMember(dest => dest.ImagesIds, opt =>
                    opt.MapFrom(src => src.Images.Select(image => image.Id)));
        }
    }
}
