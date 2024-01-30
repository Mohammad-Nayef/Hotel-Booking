using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Models.City;

namespace HotelBooking.Domain.Models.Hotel
{
    /// <summary>
    /// Hotel model for a detailed page for user.
    /// </summary>
    public class HotelPageDTO : Entity
    {
        /// <inheritdoc cref="HotelDTO.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="HotelDTO.BriefDescription"/>
        public string BriefDescription { get; set; }

        /// <inheritdoc cref="HotelDTO.FullDescription"/>
        public string FullDescription { get; set; }

        /// <inheritdoc cref="HotelDTO.StarRating"/>
        public float StarRating { get; set; }

        /// <inheritdoc cref="HotelDTO.Geolocation"/>
        public string Geolocation { get; set; }

        public CityForUserDTO City { get; set; }

        /// <summary>
        /// The highest available discount at the moment.
        /// </summary>
        public DiscountDTO CurrentDiscount { get; set; }
    }
}
