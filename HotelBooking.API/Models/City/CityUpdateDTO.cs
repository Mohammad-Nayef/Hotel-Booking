using HotelBooking.Domain.Models.City;

namespace HotelBooking.Api.Models.City
{
    /// <summary>
    /// Model to update city.
    /// </summary>
    public class CityUpdateDTO
    {
        /// <inheritdoc cref="CityDTO.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="CityDTO.CountryName"/>
        public string CountryName { get; set; }

        /// <inheritdoc cref="CityDTO.PostOffice"/>
        public string PostOffice { get; set; }
    }
}
