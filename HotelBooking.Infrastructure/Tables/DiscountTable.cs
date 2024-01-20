﻿namespace HotelBooking.Infrastructure.Tables
{
    internal class DiscountTable : DbEntity
    {
        public string? Reason { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public float AmountPercent { get; set; }
        public Guid HotelId { get; set; }
        public HotelTable Hotel { get; set; }
    }
}