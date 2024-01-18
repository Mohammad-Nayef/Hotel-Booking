using AutoMapper;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Profiles
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
