using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.Room
{
    /// <summary>
    /// Room model to view for admin.
    /// </summary>
    public class RoomForAdminDTO : Entity
    {
        /// <inheritdoc cref="RoomDTO.Number"/>
        public double Number { get; set; }

        /// <inheritdoc cref="RoomDTO.Type"/>
        public string Type { get; set; }

        /// <inheritdoc cref="RoomDTO.AdultsCapacity"/>
        public int AdultsCapacity { get; set; }

        /// <inheritdoc cref="RoomDTO.ChildrenCapacity"/>
        public int ChildrenCapacity { get; set; }

        /// <inheritdoc cref="RoomDTO.IsAvailable"/>
        public bool IsAvailable { get; set; }

        /// <inheritdoc cref="RoomDTO.CreationDate"/>
        public DateTime CreationDate { get; set; }

        /// <inheritdoc cref="RoomDTO.ModificationDate"/>
        public DateTime ModificationDate { get; set; }
    }
}
