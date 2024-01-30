using HotelBooking.Domain.Models.City;

namespace HotelBooking.Infrastructure.Tables
{
    /// <inheritdoc cref="CityDTO"/>
    internal class CityTable : DbEntity
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

        public List<HotelTable> Hotels { get; } = new();
    }
}
