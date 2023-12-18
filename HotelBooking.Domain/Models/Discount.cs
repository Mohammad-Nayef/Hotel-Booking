using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class Discount : Entity
    {
        public string? Reason { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public float AmountPercent { get; set; }
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
