using AutoMapper;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreationDTO, UserDTO>();
            CreateMap<UserCreationDTO, UserCreationResponseDTO>();
        }
    }
}
