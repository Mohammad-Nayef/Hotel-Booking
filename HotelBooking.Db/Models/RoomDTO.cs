using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Db.Models
{
    internal class RoomDTO : Entity
    {
        internal double RoomNumber { get; set; }
        internal string Type { get; set; }
        internal int AdultsCapacity { get; set; }
        internal int ChildrenCapacity { get; set; }
        internal string BriefDescription { get; set; }
        internal decimal PricePerNight { get; set; }
        internal Guid HotelId { get; set; }
        internal HotelDTO Hotel { get; set; }
        internal List<ImageDTO> Images { get; } = new();
        internal List<CartItemDTO> CartItems { get; } = new();
        internal List<BookingDTO> Bookings { get; } = new();
    }
}
