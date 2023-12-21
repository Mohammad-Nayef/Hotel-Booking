using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class BookingDTO : Entity
    {
        public DateTime CreationDate { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
    }
}
