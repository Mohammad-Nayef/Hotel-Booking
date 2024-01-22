using HotelBooking.Domain.Models;

namespace HotelBooking.Infrastructure.Tables
{
    /// <inheritdoc cref="HotelReviewDTO"/>
    internal class HotelReviewTable : DbEntity
    {
        /// <inheritdoc cref="HotelReviewDTO.Content"/>
        public string Content { get; set; }

        /// <inheritdoc cref="HotelReviewDTO.CreationDate"/>
        public DateTime CreationDate { get; set; }

        /// <inheritdoc cref="HotelReviewDTO.HotelId"/>
        public Guid HotelId { get; set; }

        public HotelTable Hotel { get; set; }

        /// <inheritdoc cref="HotelReviewDTO.UserId"/>
        public Guid UserId { get; set; }

        public UserTable User { get; set; }
    }
}
