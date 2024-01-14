using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models.User;

namespace HotelBooking.Db.Profiles
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
