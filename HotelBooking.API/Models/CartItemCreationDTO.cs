using System.Text.Json.Serialization;

namespace HotelBooking.Api.Models
{
    /// <summary>
    /// Properties for creating new cart item.
    /// </summary>
    public class CartItemCreationDTO
    {
        /// <summary>
        /// Id of the room in the cart.
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// User who added the room to the cart.
        /// </summary>
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
