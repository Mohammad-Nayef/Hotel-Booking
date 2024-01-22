using System.Text.Json.Serialization;

namespace HotelBooking.Api.Models
{
    /// <summary>
    /// Model for creating new hotel review.
    /// </summary>
    public class HotelReviewCreationDTO
    {
        /// <summary>
        /// Textual content of the review.
        /// Must be of length between 1 and 300.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Id of the hotel that contains the review.
        /// </summary>
        public Guid HotelId { get; set; }

        /// <summary>
        /// Id of the user who created the review.
        /// </summary>
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
