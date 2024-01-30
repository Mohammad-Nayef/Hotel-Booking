using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.Room
{
    /// <summary>
    /// Room model to view for user.
    /// </summary>
    public class RoomForUserDTO : Entity
    {
        /// <inheritdoc cref="RoomDTO.Number"/>
        public double Number { get; set; }

        /// <inheritdoc cref="RoomDTO.Type"/>
        public string Type { get; set; }

        /// <inheritdoc cref="RoomDTO.AdultsCapacity"/>
        public int AdultsCapacity { get; set; }

        /// <inheritdoc cref="RoomDTO.ChildrenCapacity"/>
        public int ChildrenCapacity { get; set; }

        /// <inheritdoc cref="RoomDTO.BriefDescription"/>
        public string BriefDescription { get; set; }

        /// <inheritdoc cref="RoomDTO.PricePerNight"/>
        public decimal PricePerNight { get; set; }

        /// <summary>
        /// The highest available discount at the moment.
        /// </summary>
        public DiscountDTO CurrentDiscount { get; set; }
    }
}
