using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Api.Models.City
{
    /// <summary>
    /// Properties of response for creating new city.
    /// </summary>
    public class CityCreationResponseDTO : Entity
    {
        /// <summary>
        /// Name of the city.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of the country that contains the city.
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Post office of the city.
        /// </summary>
        public string PostOffice { get; set; }
    }
}
