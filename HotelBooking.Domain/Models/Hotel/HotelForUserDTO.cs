﻿namespace HotelBooking.Domain.Models.Hotel
{
    public class HotelForUserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BriefDescription { get; set; }
        public float StarRating { get; set; }
        public Guid? ThumbnailId { get; set; }
    }
}