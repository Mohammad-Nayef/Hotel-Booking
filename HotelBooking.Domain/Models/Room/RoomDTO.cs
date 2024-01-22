using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.Room
{
    /// <inheritdoc cref="Entities.Room"/>
    public class RoomDTO : Entity
    {
        /// <inheritdoc cref="Entities.Room.Number"/>
        public double Number { get; set; }

        /// <inheritdoc cref="Entities.Room.Type"/>
        public string Type { get; set; }

        /// <inheritdoc cref="Entities.Room.AdultsCapacity"/>
        public int AdultsCapacity { get; set; }

        /// <inheritdoc cref="Entities.Room.ChildrenCapacity"/>
        public int ChildrenCapacity { get; set; }

        /// <inheritdoc cref="Entities.Room.BriefDescription"/>
        public string BriefDescription { get; set; }

        /// <inheritdoc cref="Entities.Room.PricePerNight"/>
        public decimal PricePerNight { get; set; }

        /// <inheritdoc cref="Entities.Room.CreationDate"/>
        public DateTime CreationDate { get; set; }

        /// <inheritdoc cref="Entities.Room.ModificationDate"/>
        public DateTime ModificationDate { get; set; }

        /// <summary>
        /// Id of the hotel that contains the room.
        /// </summary>
        public Guid HotelId { get; set; }
    }
}
