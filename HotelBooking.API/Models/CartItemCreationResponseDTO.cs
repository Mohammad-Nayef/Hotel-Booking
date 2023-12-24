namespace HotelBooking.Api.Models
{
    public class CartItemCreationResponseDTO
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
    }
}
