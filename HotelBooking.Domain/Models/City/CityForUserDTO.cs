using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.City
{
    /// <summary>
    /// City model to view for user.
    /// </summary>
    public class CityForUserDTO : Entity
    {
        /// <inheritdoc cref="CityDTO.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="CityDTO.CountryName"/>
        public string CountryName { get; set; }

        /// <inheritdoc cref="CityDTO.PostOffice"/>
        public string PostOffice { get; set; }
    }
}
