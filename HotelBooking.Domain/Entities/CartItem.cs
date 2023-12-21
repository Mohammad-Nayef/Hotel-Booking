using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Entities
{
    public class CartItem : Entity
    {
        public DateTime AddingDate { get; set; }
        public Room Room { get; set; }
        public User User { get; set; }
    }
}
