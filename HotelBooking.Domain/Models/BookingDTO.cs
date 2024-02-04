using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    /// <inheritdoc cref="Entities.Booking"/>
    public class BookingDTO : Entity
    {
        /// <inheritdoc cref="Entities.Booking.CreationDate"/>
        public DateTime CreationDate { get; set; }

        /// <inheritdoc cref="Entities.Booking.StartingDate"/>
        public DateTime StartingDate { get; set; }

        /// <inheritdoc cref="Entities.Booking.EndingDate"/>
        public DateTime EndingDate { get; set; }

        /// <inheritdoc cref="Entities.Booking.Price"/>
        public decimal? Price { get; set; }

        /// <summary>
        /// Id of the room for the booking.
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// Id of the user who created the booking.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
