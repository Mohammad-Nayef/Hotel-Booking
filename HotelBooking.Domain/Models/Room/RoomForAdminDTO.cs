﻿namespace HotelBooking.Domain.Models.Room
{
    public class RoomForAdminDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public double Number { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}