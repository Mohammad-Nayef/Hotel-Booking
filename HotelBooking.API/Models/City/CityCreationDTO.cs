namespace HotelBooking.Api.Models.City
{
    /// <summary>
    /// Properties for creating new city.
    /// </summary>
    public class CityCreationDTO
    {
        /// <summary>
        /// Name of the city.
        /// Must be of length between 1 and 100.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of the country that contains the city.
        /// Must be of length between 1 and 100.
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Post office of the city.
        /// Must be of length between 1 and 100.
        /// </summary>
        public string PostOffice { get; set; }
    }
}
