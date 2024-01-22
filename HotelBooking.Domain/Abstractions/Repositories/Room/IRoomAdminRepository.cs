using System.Linq.Expressions;
using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Domain.Abstractions.Repositories.Room
{
    /// <summary>
    /// Responsible for managing storage for rooms by admin.
    /// </summary>
    public interface IRoomAdminRepository
    {
        /// <summary>
        /// Get list of rooms for admin by page.
        /// </summary>
        IEnumerable<RoomForAdminDTO> GetByPage(int itemsToSkip, int itemsToTake);

        /// <summary>
        /// Search for rooms by page for admin.
        /// </summary>
        /// <param name="searchExpression">Expression to define the criteria of the search.</param>
        IEnumerable<RoomForAdminDTO> SearchByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<RoomForAdminDTO, bool>> searchExpression);

        /// <summary>
        /// Get count of search expression for rooms by page for admin.
        /// </summary>
        /// <param name="searchExpression">Expression to define the criteria of the search.</param>
        Task<int> GetSearchCountAsync(
            Expression<Func<RoomForAdminDTO, bool>> searchExpression);
    }
}
