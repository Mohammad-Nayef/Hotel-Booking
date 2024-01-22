using System.Text.Json.Serialization;

namespace HotelBooking.Api.Models
{
    /// <summary>
    /// Model to create new booking.
    /// </summary>
    public class BookingCreationDTO
    {
        /// <summary>
        /// Starting time for the booking interval.
        /// Can't be in the past.
        /// </summary>
        public DateTime StartingDate { get; set; }

        /// <summary>
        /// Ending time for the booking interval.
        /// Cannot be in the past and must be after StartingDate.
        /// </summary>
        public DateTime EndingDate { get; set; }

        /// <summary>
        /// Id of the room for the booking.
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// Id of the user who created the booking.
        /// </summary>
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
