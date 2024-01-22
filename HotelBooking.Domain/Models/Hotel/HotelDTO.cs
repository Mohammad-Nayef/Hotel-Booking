using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.Hotel
{
    /// <inheritdoc cref="Entities.Hotel"/>
    public class HotelDTO : Entity
    {
        /// <inheritdoc cref="Entities.Hotel.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="Entities.Hotel.BriefDescription"/>
        public string BriefDescription { get; set; }

        /// <inheritdoc cref="Entities.Hotel.FullDescription"/>
        public string FullDescription { get; set; }

        /// <inheritdoc cref="Entities.Hotel.StarRating"/>
        public float StarRating { get; set; }

        /// <inheritdoc cref="Entities.Hotel.OwnerName"/>
        public string OwnerName { get; set; }

        /// <inheritdoc cref="Entities.Hotel.Geolocation"/>
        public string Geolocation { get; set; }

        /// <inheritdoc cref="Entities.Hotel.CreationDate"/>
        public DateTime CreationDate { get; set; }

        /// <inheritdoc cref="Entities.Hotel.ModificationDate"/>
        public DateTime ModificationDate { get; set; }

        /// <summary>
        /// Id of the city that contains the hotel.
        /// </summary>
        public Guid CityId { get; set; }
    }
}
