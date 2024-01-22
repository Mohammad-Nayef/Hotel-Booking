using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Entities
{
    /// <summary>
    /// Visit that's created when a user visits an hotel.
    /// </summary>
    public class HotelVisit : Entity
    {
        /// <summary>
        /// Time when the user visited the hotel.
        /// </summary>
        public DateTime Date { get; set; }

        public Hotel Hotel { get; set; }

        public User User { get; set; }
    }
}
