using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Db.Models
{
    internal class BookingDTO : Entity
    {
        internal DateTime CreationDate { get; set; }
        internal DateTime StartingDate { get; set; }
        internal DateTime EndingDate { get; set; }
        internal Guid RoomId { get; set; }
        internal RoomDTO Room { get; set; }
        internal Guid UserId { get; set; }
        internal UserDTO User { get; set; }
    }
}
