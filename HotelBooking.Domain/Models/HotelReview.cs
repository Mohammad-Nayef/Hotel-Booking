using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class HotelReview : Entity
    {
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public Hotel Hotel { get; set; }
        public User User { get; set; }
    }
}
