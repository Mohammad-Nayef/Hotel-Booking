using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.Hotel
{
    /// <summary>
    /// Hotel model to view for admin.
    /// </summary>
    public class HotelForAdminDTO : Entity
    {
        /// <inheritdoc cref="HotelDTO.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="HotelDTO.StarRating"/>
        public float StarRating { get; set; }

        /// <inheritdoc cref="HotelDTO.OwnerName"/>
        public string OwnerName { get; set; }

        /// <inheritdoc cref="HotelDTO.CreationDate"/>
        public DateTime CreationDate { get; set; }

        /// <inheritdoc cref="HotelDTO.ModificationDate"/>
        public DateTime ModificationDate { get; set; }

        /// <summary>
        /// Number of rooms that exist in the hotel.
        /// </summary>
        public int NumberOfRooms { get; set; }
    }
}
