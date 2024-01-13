using System.Text.Json.Serialization;

namespace HotelBooking.Api.Models.CartItem
{
    public class CartItemCreationDTO
    {
        public Guid RoomId { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
