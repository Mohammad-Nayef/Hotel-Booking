using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class HotelReviewDTO : Entity
    {
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid HotelId { get; set; }
        public Guid UserId { get; set; }
    }
}
