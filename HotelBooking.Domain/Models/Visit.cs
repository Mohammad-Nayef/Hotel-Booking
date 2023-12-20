﻿using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class Visit : Entity
    {
        public DateTime Date { get; set; }
        public Hotel Hotel { get; set; }
        public User User { get; set; }
    }
}
