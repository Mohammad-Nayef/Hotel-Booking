using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Models.User;

namespace HotelBooking.Domain.Models
{
    /// <summary>
    /// Model of hotel review to view in hotel page.
    /// </summary>
    public class ReviewForHotelPageDTO : Entity
    {
        /// <inheritdoc cref="HotelReviewDTO.Content"/>
        public string Content { get; set; }

        /// <inheritdoc cref="HotelReviewDTO.CreationDate"/>
        public DateTime CreationDate { get; set; }

        public UserForHotelReviewDTO User { get; set; }
    }
}
