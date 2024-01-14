using AutoMapper;
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
        }
    }
}
