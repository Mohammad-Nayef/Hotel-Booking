using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Responsible for managing main operations for storage for hotel review storage.
    /// </summary>
    public interface IHotelReviewRepository
    {
        /// <summary>
        /// Store new hotel review.
        /// </summary>
        Task AddAsync(HotelReviewDTO newReview);

        /// <summary>
        /// Determine whether an hotel review exists by user and hotel or not.
        /// </summary>
        Task<bool> ExistsByUserAndHotelAsync(Guid userId, Guid hotelId);

        /// <summary>
        /// Get list of reviews for an hotel by page.
        /// </summary>
        IEnumerable<ReviewForHotelPageDTO> GetReviewsByHotelByPage(
            Guid hotelId, int itemsToSkip, int itemsToTake);

        /// <summary>
        /// Get number of reviews for an hotel.
        /// </summary>
        Task<int> GetReviewsByHotelCountAsync(Guid hotelId);
    }
}
