using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    /// <inheritdoc cref="Entities.CartItem"/>
    public class CartItemDTO : Entity
    {
        /// <inheritdoc cref="Entities.CartItem.AddingDate"/>
        public DateTime AddingDate { get; set; }

        /// <summary>
        /// Id of the room in the cart.
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// User who added the room to the cart.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
