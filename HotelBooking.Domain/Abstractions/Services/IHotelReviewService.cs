using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    /// <summary>
    /// Responsible for processing main operations for hotel review.
    /// </summary>
    public interface IHotelReviewService
    {
        /// <summary>
        /// Add new hotel review to the system.
        /// </summary>
        Task AddAsync(HotelReviewDTO newReview);

        /// <summary>
        /// Get list of reviews for an hotel by page.
        /// </summary>
        IEnumerable<ReviewForHotelPageDTO> GetReviewsByHotelByPage(
            Guid hotelId, PaginationDTO pagination);

        /// <summary>
        /// Get number of reviews for an hotel.
        /// </summary>
        Task<int> GetReviewsByHotelCountAsync(Guid hotelId);
    }
}
