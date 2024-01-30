using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Entities
{
    /// <summary>
    /// Details about an hotel.
    /// </summary>
    public class Hotel : Entity
    {
        /// <summary>
        /// Name of the hotel.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="HotelConstants.MinNameLength"/> and 
        /// <see cref="HotelConstants.MaxNameLength"/>
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// Small summary about the hotel.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="HotelConstants.MinBriefDescriptionLength"/> and 
        /// <see cref="HotelConstants.MaxBriefDescriptionLength"/>
        /// </remarks>
        public string BriefDescription { get; set; }

        /// <summary>
        /// Detailed description about the hotel.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="HotelConstants.MinFullDescriptionLength"/> and 
        /// <see cref="HotelConstants.MaxFullDescriptionLength"/>
        /// </remarks>
        public string FullDescription { get; set; }

        /// <summary>
        /// Star rating of the hotel.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="HotelConstants.MinStarRating"/> and 
        /// <see cref="HotelConstants.MaxStarRating"/>
        /// </remarks>
        public float StarRating { get; set; }

        /// <summary>
        /// Name of the owner of the hotel.
        /// </summary>
        /// <remarks>Can contain multiple separated names in case of multiple owners.</remarks>
        public string OwnerName { get; set; }

        /// <summary>
        /// Geolocation coordinates of the hotel in the format "latitude, longitude".
        /// </summary>
        /// <remarks>
        /// Latitude and longitude are expressed as decimal numbers separated by a comma. 
        /// Example: "37.7749, -122.4194". It must match 
        /// <see cref="HotelConstants.GeolocationRegex"/>.
        /// </remarks>
        public string Geolocation { get; set; }

        /// <summary>
        /// Time when the hotel is added to the system.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Time of the last modification for the hotel in the system.
        /// </summary>
        public DateTime ModificationDate { get; set; }

        public List<HotelReview> Reviews { get; set; }

        public City City { get; set; }

        public List<Room> Rooms { get; set; }

        public List<Discount> Discounts { get; set; }

        public List<HotelVisit> Visits { get; set; }
    }
}
