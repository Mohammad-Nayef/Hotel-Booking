﻿using AutoMapper;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Profiles
{
    internal class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ImageDTO, ImageTable>();
        }
    }
}
