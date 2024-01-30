using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Entities
{
    /// <summary>
    /// Details about a city.
    /// </summary>
    public class City : Entity
    {
        /// <summary>
        /// Name of the city.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="CityConstants.MinNameLength"/> and 
        /// <see cref="CityConstants.MaxNameLength"/>.
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// Name of the country that contains the city.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="CityConstants.MinCountryNameLength"/> and 
        /// <see cref="CityConstants.MaxCountryNameLength"/>.
        /// </remarks>
        public string CountryName { get; set; }

        /// <summary>
        /// Post office of the city.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="CityConstants.MinPostOfficeLength"/> and 
        /// <see cref="CityConstants.MaxPostOfficeLength"/>.
        /// </remarks>
        public string PostOffice { get; set; }

        /// <summary>
        /// Time when the city is added to the system.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Time of the last modification for the city in the system.
        /// </summary>
        public DateTime ModificationDate { get; set; }

        public List<Hotel> Hotels { get; set; }
    }
}
