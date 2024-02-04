using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Domain.Abstractions.Repositories.Hotel
{
    /// <summary>
    /// Responsible for managing hotel storage by user.
    /// </summary>
    public interface IHotelUserRepository
    {
        /// <summary>
        /// Search for hotels for user by page based on <see cref="HotelSearchDTO"/>.
        /// </summary>
        IEnumerable<HotelForUserDTO> SearchForUserByPage(
            HotelSearchDTO hotelSearch, int itemsToSkip, int itemsToTake);

        /// <summary>
        /// Get number of hotels by search for user based on <see cref="HotelSearchDTO"/>.
        /// </summary>
        int GetSearchForUserCount(HotelSearchDTO hotelSearch);

        /// <summary>
        /// Get hotel with details for user by Id.
        /// </summary>
        Task<HotelPageDTO> GetHotelPageAsync(Guid id);

        /// <summary>
        /// Get list of currently available rooms in an hotel.
        /// </summary>
        IEnumerable<RoomForUserDTO> GetAvailableRooms(
            Guid id, int itemsToSkip, int itemsToTake);

        /// <summary>
        /// Get number of currently available rooms in an hotel.
        /// </summary>
        int GetAvailableRoomsCount(Guid id);

        /// <summary>
        /// Get list of hotels visited by a user.
        /// </summary>
        /// <remarks>
        /// Only visits after <see cref="HotelVisitConstants.LeastRecentVisitDate"/> 
        /// are considered.
        /// </remarks>
        IEnumerable<VisitedHotelDTO> GetRecentlyVisitedByPage(
            Guid userId, int itemsToSkip, int itemsToTake);

        /// <summary>
        /// Get number of visited hotels by a user.
        /// </summary>
        /// <remarks>
        /// Only visits after <see cref="HotelVisitConstants.LeastRecentVisitDate"/> 
        /// are considered.
        /// </remarks>
        Task<int> GetVisitedCountAsync(Guid userId);
    }
}
