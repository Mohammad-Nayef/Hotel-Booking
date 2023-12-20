using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Db.Models
{
    internal class CartItemDTO : Entity
    {
        internal DateTime AddingDate { get; set; }
        internal Guid RoomId { get; set; }
        internal RoomDTO Room { get; set; }
        internal Guid UserId { get; set; }
        internal UserDTO User { get; set; }
    }
}
