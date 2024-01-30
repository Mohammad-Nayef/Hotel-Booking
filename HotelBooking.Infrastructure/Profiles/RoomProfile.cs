using AutoMapper;
using HotelBooking.Domain.Models.Room;
using HotelBooking.Infrastructure.Extensions;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Profiles
{
    internal class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomDTO, RoomTable>()
                .ReverseMap();

            CreateMap<RoomTable, RoomForUserDTO>()
                .ForMember(dest => dest.CurrentDiscount, opt =>
                    opt.MapFrom(src => src.Hotel.Discounts.GetHighestActive()));
        }
    }
}
