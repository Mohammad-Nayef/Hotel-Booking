using HotelBooking.Domain.Models;

namespace HotelBooking.Infrastructure.Tables
{
    /// <inheritdoc cref="BookingDTO"/>
    internal class BookingTable : DbEntity
    {
        /// <inheritdoc cref="BookingDTO.CreationDate"/>
        public DateTime CreationDate { get; set; }

        /// <inheritdoc cref="BookingDTO.StartingDate"/>
        public DateTime StartingDate { get; set; }

        /// <inheritdoc cref="BookingDTO.EndingDate"/>
        public DateTime EndingDate { get; set; }

        /// <inheritdoc cref="BookingDTO.Price"/>
        public decimal Price { get; set; }

        /// <inheritdoc cref="BookingDTO.RoomId"/>
        public Guid RoomId { get; set; }

        public RoomTable Room { get; set; }

        /// <inheritdoc cref="BookingDTO.UserId"/>
        public Guid UserId { get; set; }

        public UserTable User { get; set; }
    }
}
