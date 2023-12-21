using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class CartItemDTO : Entity
    {
        public DateTime AddingDate { get; set; }
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
    }
}
