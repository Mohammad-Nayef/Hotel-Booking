using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Domain.Abstractions.Services.Hotel
{
    /// <summary>
    /// Responsible for managing operations on hotel for user.
    /// </summary>
    public interface IHotelUserService
    {
        /// <summary>
        /// Get list of featured hotels by page.
        /// </summary>
        Task<IEnumerable<FeaturedHotelDTO>> GetFeaturedHotelsByPageAsync(PaginationDTO pagination);

        /// <summary>
        /// Get number of featured hotels.
        /// </summary>
        /// <returns></returns>
        int GetFeaturedHotelsCount();

        /// <summary>
        /// Search for hotels by user based on <see cref="HotelSearchDTO"/> by page.
        /// </summary>
        /// <param name="hotelSearch">Properties to define the criteria of the search.</param>
        Task<IEnumerable<HotelForUserDTO>> SearchByPageAsync(
            HotelSearchDTO hotelSearch, PaginationDTO pagination);

        /// <summary>
        /// Get number of hotels of search result based on <see cref="HotelSearchDTO"/>.
        /// </summary>
        /// <param name="hotelSearch">Properties to define the criteria of the search.</param>
        int GetSearchCount(HotelSearchDTO hotelSearch);

        /// <summary>
        /// Get detailed model of an hotel for a given user to create a visit for the 
        /// hotel by the user.
        /// </summary>
        Task<HotelPageDTO> GetHotelPageAsync(Guid hotelId, Guid userId);

        /// <summary>
        /// Get list of reviews for an hotel by page.
        /// </summary>
        Task<IEnumerable<ReviewForHotelPageDTO>> GetReviewsByPageAsync(
            Guid id, PaginationDTO pagination);

        /// <summary>
        /// Get number of reviews for an hotel.
        /// </summary>
        Task<int> GetReviewsCountAsync(Guid id);

        /// <summary>
        /// Get number of available rooms for an hotel.
        /// </summary>
        Task<IEnumerable<RoomForUserDTO>> GetAvailableRoomsAsync(
            Guid id, PaginationDTO pagination);

        /// <summary>
        /// Get number of available rooms for an hotel.
        /// </summary>
        int GetAvailableRoomsCount(Guid id);

        /// <summary>
        /// Get list of hotels ordered descending by its visiting date for a user by page.
        /// </summary>
        Task<IEnumerable<VisitedHotelDTO>> GetRecentlyVisitedByPageAsync(
            Guid userId, PaginationDTO pagination);

        /// <summary>
        /// Get number of recently visited hotels by a user.
        /// </summary>
        Task<int> GetRecentlyVisitedCountAsync(Guid userId);
    }
}
