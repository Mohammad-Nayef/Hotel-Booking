using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    /// <inheritdoc cref="Entities.HotelVisit"/>
    public class HotelVisitDTO : Entity
    {
        /// <inheritdoc cref="Entities.HotelVisit.Date"/>
        public DateTime Date { get; set; }

        /// <summary>
        /// Id of the visited hotel.
        /// </summary>
        public Guid HotelId { get; set; }

        /// <summary>
        /// Id of the user who visited the hotel.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
