﻿namespace HotelBooking.Api.Models.Room
{
    public class RoomCreationDTO
    {
        public double RoomNumber { get; set; }
        public string Type { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public string BriefDescription { get; set; }
        public decimal PricePerNight { get; set; }
        public Guid HotelId { get; set; }
    }
}