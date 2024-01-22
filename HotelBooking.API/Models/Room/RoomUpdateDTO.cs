namespace HotelBooking.Api.Models.Room
{
    /// <summary>
    /// Model to update room.
    /// </summary>
    public class RoomUpdateDTO
    {
        /// <summary>
        /// Number of the room which is identified by the hotel.
        /// </summary>
        public double Number { get; set; }

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
    }
}
