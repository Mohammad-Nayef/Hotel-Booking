using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Api.Models.CartItem
{
    /// <summary>
    /// Properties of response for creating new cart item.
    /// </summary>
    public class CartItemCreationResponseDTO : Entity
    {
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
