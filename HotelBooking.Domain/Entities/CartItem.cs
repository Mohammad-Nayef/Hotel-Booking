using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Entities
{
    /// <summary>
    /// Room that's stored by user for later browsing.
    /// </summary>
    public class CartItem : Entity
    {
        /// <summary>
        /// Time when the item is added to the cart.
        /// </summary>
        public DateTime AddingDate { get; set; }

        public Room Room { get; set; }

        public User User { get; set; }
    }
}
