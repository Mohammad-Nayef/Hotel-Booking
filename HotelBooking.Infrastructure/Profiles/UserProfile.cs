using AutoMapper;
using HotelBooking.Domain.Models.User;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Profiles
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, UserTable>()
                .ReverseMap();

            CreateMap<UserTable, UserForHotelReviewDTO>();
        }
    }
}
