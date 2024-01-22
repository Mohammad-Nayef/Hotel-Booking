using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Models.City;

namespace HotelBooking.Domain.Models.Hotel
{
    /// <summary>
    /// Model of hotel with special offers.
    /// </summary>
    public class FeaturedHotelDTO : Entity
    {
        /// <inheritdoc cref="HotelDTO.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="CityDTO.Name"/>
        public string? CityName { get; set; }

        /// <inheritdoc cref="CityDTO.CountryName"/>
        public string? CountryName { get; set; }

        /// <inheritdoc cref="HotelDTO.StarRating"/>
        public float? StarRating { get; set; }

        /// <summary>
        /// Id of the thumbnail of the image that represents the hotel.
        /// </summary>
        public Guid? ThumbnailId { get; set; }

        /// <summary>
        /// The highest available discount at the moment.
        /// </summary>
        public DiscountDTO CurrentDiscount { get; set; }
    }
}
