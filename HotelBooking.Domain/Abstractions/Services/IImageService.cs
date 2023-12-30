﻿using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IImageService
    {
        Task AddForCityAsync(Guid cityId, IEnumerable<Image> images);
        Task AddForHotelAsync(Guid hotelId, IEnumerable<Image> images);
    }
}
