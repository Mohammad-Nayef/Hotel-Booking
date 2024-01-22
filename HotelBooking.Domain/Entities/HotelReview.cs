using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Entities
{
    /// <summary>
    /// Hotel review created by a user.
    /// </summary>
    public class HotelReview : Entity
    {
        /// <summary>
        /// Textual content of the review.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="HotelReviewConstants.MinContentLength"/> and 
        /// <see cref="HotelReviewConstants.MaxContentLength"/>.
        /// </remarks>
        public string Content { get; set; }

        /// <summary>
        /// Time when the review is created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        public Hotel Hotel { get; set; }

        public User User { get; set; }
    }
}
