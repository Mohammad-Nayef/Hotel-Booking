using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.City
{
    /// <summary>
    /// Model to view popular city.
    /// </summary>
    public class PopularCityDTO : Entity
    {
        /// <inheritdoc cref="CityDTO.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="CityDTO.CountryName"/>
        public string CountryName { get; set; }

        /// <summary>
        /// Id of a thumbnail of an image to represent the city.
        /// </summary>
        public Guid? ThumbnailId { get; set; }
    }
}
