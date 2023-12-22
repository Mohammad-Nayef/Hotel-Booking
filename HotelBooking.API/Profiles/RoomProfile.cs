using AutoMapper;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomCreationDTO, RoomDTO>();
            CreateMap<RoomCreationDTO, RoomForCreationResponseDTO>();
            CreateMap<RoomDTO, RoomUpdateDTO>();
            CreateMap<RoomUpdateDTO, RoomDTO>();
        }
    }
}
