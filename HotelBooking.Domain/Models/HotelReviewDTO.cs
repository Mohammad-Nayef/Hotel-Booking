using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    /// <inheritdoc cref="Entities.HotelReview"/>
    public class HotelReviewDTO : Entity
    {
        /// <inheritdoc cref="Entities.HotelReview.Content"/>
        public string Content { get; set; }

        /// <inheritdoc cref="Entities.HotelReview.CreationDate"/>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Id of the hotel that contains the review.
        /// </summary>
        public Guid HotelId { get; set; }

        /// <summary>
        /// Id of the user who created the review.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
