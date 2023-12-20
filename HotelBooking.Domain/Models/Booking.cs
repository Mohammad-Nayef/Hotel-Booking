﻿using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class Booking : Entity
    {
        public DateTime CreationDate { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public Room Room { get; set; }
        public User User { get; set; }
    }
}
