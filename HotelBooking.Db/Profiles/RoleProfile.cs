using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Profiles
{
    internal class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDTO, RoleTable>()
                .ReverseMap();
        }
    }
}
