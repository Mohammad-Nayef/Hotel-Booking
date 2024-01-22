using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Entities
{
    /// <summary>
    /// A booking that's created by a user for a room in a defined interval.
    /// </summary>
    public class Booking : Entity
    {
        /// <summary>
        /// Time when the booking is created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Starting time for the booking interval.
        /// </summary>
        /// <remarks>Can't be in the past.</remarks>
        public DateTime StartingDate { get; set; }

        /// <summary>
        /// Ending time for the booking interval.
        /// </summary>
        /// <remarks>Cannot be in the past and must be after <see cref="StartingDate"/>.</remarks>
        public DateTime EndingDate { get; set; }

        /// <summary>
        /// Total price for the booking regarding price per night, number of nights and discounts.
        /// </summary>
        public decimal Price { get; set; }

        public Room Room { get; set; }

        public User User { get; set; }
    }
}
