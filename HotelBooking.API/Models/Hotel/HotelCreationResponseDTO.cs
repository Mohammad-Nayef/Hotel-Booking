using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Api.Models.Hotel
{
    /// <summary>
    /// Response for creating new hotel.
    /// </summary>
    public class HotelCreationResponseDTO : Entity
    {
        /// <summary>
        /// Name of the hotel.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Small summary about the hotel.
        /// </summary>
        public string BriefDescription { get; set; }

        /// <summary>
        /// Detailed description about the hotel.
        /// </summary>
        public string FullDescription { get; set; }

        /// <summary>
        /// Star rating of the hotel.
        /// </summary>
        public float StarRating { get; set; }

        /// <summary>
        /// Name of the owner of the hotel.
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// Geolocation coordinates of the hotel in the format "latitude, longitude".
        /// </summary>
        public string Geolocation { get; set; }

        /// <summary>
        /// Id of the city that contains the hotel.
        /// </summary>
        public Guid CityId { get; set; }
    }
}
