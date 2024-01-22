using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.Hotel
{
    /// <summary>
    /// Details about hotel for user.
    /// </summary>
    public class HotelForUserDTO : Entity
    {
        /// <inheritdoc cref="HotelDTO.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="HotelDTO.BriefDescription"/>
        public string BriefDescription { get; set; }

        /// <inheritdoc cref="HotelDTO.StarRating"/>
        public float StarRating { get; set; }

        /// <summary>
        /// Id of the thumbnail of the image that represents the room.
        /// </summary>
        public Guid? ThumbnailId { get; set; }
    }
}
