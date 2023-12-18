using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class CartItem : Entity
    {
        public DateTime AddingDate { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
