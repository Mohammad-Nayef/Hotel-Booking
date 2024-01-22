namespace HotelBooking.Api.Models.Hotel
{
    /// <summary>
    /// Model to update hotel.
    /// </summary>
    public class HotelUpdateDTO
    {
        /// <summary>
        /// Name of the hotel.
        /// Must be of length between 1 and 100.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of the owner of the hotel.
        /// Can contain multiple separated names in case of multiple owners.
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// Geolocation coordinates of the hotel in the format "latitude, longitude".
        /// Latitude and longitude are expressed as decimal numbers separated by a comma. 
        /// Example: "37.7749, -122.4194". It must match: 
        /// ^((\-?|\+?)?\d+(\.\d+)?),\s*((\-?|\+?)?\d+(\.\d+)?)$
        /// </summary>
        public string Geolocation { get; set; }

        /// <summary>
        /// Id of the city that contains the hotel.
        /// </summary>
        public Guid CityId { get; set; }
    }
}
