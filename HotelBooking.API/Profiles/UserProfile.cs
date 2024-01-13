using AutoMapper;
using HotelBooking.Api.Models.User;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Profiles
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreationDTO, UserDTO>();
            CreateMap<UserCreationDTO, UserCreationResponseDTO>();
        }
    }
}
