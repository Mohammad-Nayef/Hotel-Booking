using AutoMapper;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Profiles
{
    internal class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomCreationDTO, RoomDTO>();
            CreateMap<RoomCreationDTO, RoomCreationResponseDTO>();
            CreateMap<RoomDTO, RoomUpdateDTO>();
            CreateMap<RoomUpdateDTO, RoomDTO>();
        }
    }
}
