using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Infrastructure.Tables
{
    /// <inheritdoc cref="RoomDTO"/>
    internal class RoomTable : DbEntity
    {
        /// <inheritdoc cref="RoomDTO.Number"/>
        public double Number { get; set; }

        /// <inheritdoc cref="RoomDTO.Type"/>
        public string Type { get; set; }

        /// <inheritdoc cref="RoomDTO.AdultsCapacity"/>
        public int AdultsCapacity { get; set; }

        /// <inheritdoc cref="RoomDTO.ChildrenCapacity"/>
        public int ChildrenCapacity { get; set; }

        /// <inheritdoc cref="RoomDTO.BriefDescription"/>
        public string BriefDescription { get; set; }

        /// <inheritdoc cref="RoomDTO.PricePerNight"/>
        public decimal PricePerNight { get; set; }

        /// <inheritdoc cref="RoomDTO.CreationDate"/>
        public DateTime CreationDate { get; set; }

        /// <inheritdoc cref="RoomDTO.ModificationDate"/>
        public DateTime ModificationDate { get; set; }

        /// <inheritdoc cref="RoomDTO.HotelId"/>
        public Guid HotelId { get; set; }

        public HotelTable Hotel { get; set; }

        public List<CartItemTable> CartItems { get; } = new();

        public List<BookingTable> Bookings { get; } = new();
    }
}
