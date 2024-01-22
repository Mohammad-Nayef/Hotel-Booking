namespace HotelBooking.Api.Models.Room
{
    /// <summary>
    /// Model for creating new room.
    /// </summary>
    public class RoomCreationDTO
    {
        /// <summary>
        /// Number of the room which is identified by the hotel.
        /// </summary>
        public double Number { get; set; }

        /// <summary>
        /// Type of the room.
        /// Must be of length between 1 and 50.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Maximum number of adults the room can accommodate.
        /// Must be between 0 and 50.
        /// </summary>
        public int AdultsCapacity { get; set; }

        /// <summary>
        /// Maximum number of children the room can accommodate.
        /// Must be between 0 and 50
        /// </summary>
        public int ChildrenCapacity { get; set; }

        /// <summary>
        /// Small summary about the room.
        /// Must be of length between 1 and 150.
        /// </summary>
        public string BriefDescription { get; set; }

        /// <summary>
        /// Price of booking the room for a single day.
        /// Must not be negative.
        /// </summary>
        public decimal PricePerNight { get; set; }

        /// <summary>
        /// Id of the hotel that contains the room.
        /// </summary>
        public Guid HotelId { get; set; }
    }
}
