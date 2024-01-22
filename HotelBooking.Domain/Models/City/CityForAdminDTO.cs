using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.City
{
    /// <summary>
    /// City model to view for admin.
    /// </summary>
    public class CityForAdminDTO : Entity
    {
        /// <inheritdoc cref="CityDTO.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="CityDTO.CountryName"/>
        public string CountryName { get; set; }

        /// <inheritdoc cref="CityDTO.PostOffice"/>
        public string PostOffice { get; set; }

        /// <inheritdoc cref="CityDTO.CreationDate"/>
        public DateTime CreationDate { get; set; }

        /// <inheritdoc cref="CityDTO.ModificationDate"/>
        public DateTime ModificationDate { get; set; }

        /// <summary>
        /// Number of hotels that exist in the city.
        /// </summary>
        public int NumberOfHotels { get; set; }
    }
}
