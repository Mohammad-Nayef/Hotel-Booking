using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Api.Models.Room
{
    /// <summary>
    /// Response for creating new room.
    /// </summary>
    public class RoomCreationResponseDTO : Entity
    {
        /// <summary>
        /// Number of the room which is identified by the hotel.
        /// </summary>
        public double Number { get; set; }

        /// <summary>
        /// Type of the room.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Maximum number of adults the room can accommodate.
        /// </summary>
        public int AdultsCapacity { get; set; }

        /// <summary>
        /// Maximum number of children the room can accommodate.
        /// </summary>
        public int ChildrenCapacity { get; set; }

        /// <summary>
        /// Small summary about the room.
        /// </summary>
        public string BriefDescription { get; set; }

        /// <summary>
        /// Price of booking the room for a single day.
        /// </summary>
        public decimal PricePerNight { get; set; }

        /// <summary>
        /// Id of the hotel that contains the room.
        /// </summary>
        public Guid HotelId { get; set; }
    }
}
