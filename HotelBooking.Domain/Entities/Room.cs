using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Entities
{
    /// <summary>
    /// Details of a room inside an hotel.
    /// </summary>
    public class Room : Entity
    {
        /// <summary>
        /// Number of the room which is identified by the hotel.
        /// </summary>
        public double Number { get; set; }

        /// <summary>
        /// Type of the room.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="RoomConstants.MinTypeLength"/> and 
        /// <see cref="RoomConstants.MaxTypeLength"/>.
        /// </remarks>
        public string Type { get; set; }

        /// <summary>
        /// Maximum number of adults the room can accommodate.
        /// </summary>
        /// <remarks>
        /// Must be between <see cref="RoomConstants.MinAdultsCapacity"/> and 
        /// <see cref="RoomConstants.MaxAdultsCapacity"/>
        /// </remarks>
        public int AdultsCapacity { get; set; }

        /// <summary>
        /// Maximum number of children the room can accommodate.
        /// </summary>
        /// <remarks>
        /// Must be between <see cref="RoomConstants.MinChildrenCapacity"/> and 
        /// <see cref="RoomConstants.MaxChildrenCapacity"/>
        /// </remarks>
        public int ChildrenCapacity { get; set; }

        /// <summary>
        /// Small summary about the room.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="RoomConstants.MinBriefDescriptionLength"/> and 
        /// <see cref="RoomConstants.MaxBriefDescriptionLength"/>
        /// </remarks>
        public string BriefDescription { get; set; }

        /// <summary>
        /// Price of booking the room for a single day.
        /// </summary>
        /// <remarks>Must not be negative.</remarks>
        public decimal PricePerNight { get; set; }

        /// <summary>
        /// Time when the room is added to the system.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Time of the last modification for the room in the system.
        /// </summary>
        public DateTime ModificationDate { get; set; }

        public Hotel Hotel { get; set; }

        public List<CartItem> CartItems { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
