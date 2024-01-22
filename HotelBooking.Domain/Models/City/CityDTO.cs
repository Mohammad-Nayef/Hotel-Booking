using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.City
{
    /// <inheritdoc cref="Entities.City"/>
    public class CityDTO : Entity
    {
        /// <inheritdoc cref="Entities.City.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="Entities.City.CountryName"/>
        public string CountryName { get; set; }

        /// <inheritdoc cref="Entities.City.PostOffice"/>
        public string PostOffice { get; set; }

        /// <inheritdoc cref="Entities.City.CreationDate"/>
        public DateTime CreationDate { get; set; }

        /// <inheritdoc cref="Entities.City.ModificationDate"/>
        public DateTime ModificationDate { get; set; }
    }
}
