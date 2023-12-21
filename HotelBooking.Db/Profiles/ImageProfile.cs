using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Profiles
{
    internal class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ImageDTO, ImageTable>();
        }
    }
}
