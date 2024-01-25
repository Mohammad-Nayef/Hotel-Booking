using AutoMapper;
using HotelBooking.Api.Models.Room;
using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Api.Profiles
{
    internal class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomCreationDTO, RoomDTO>();
            CreateMap<RoomDTO, RoomUpdateDTO>()
                .ReverseMap();
        }
    }
}
