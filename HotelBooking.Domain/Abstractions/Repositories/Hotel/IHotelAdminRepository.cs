using System.Linq.Expressions;
using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Domain.Abstractions.Repositories.Hotel
{
    /// <summary>
    /// Responsible for managing storage for hotel by admin.
    /// </summary>
    public interface IHotelAdminRepository
    {
        /// <summary>
        /// Get list of hotels for admin by page.
        /// </summary>
        IEnumerable<HotelForAdminDTO> GetByPage(int itemsToSkip, int itemsToTake);

        /// <summary>
        /// Search for hotels by page for admin.
        /// </summary>
        /// <param name="searchExpression">Expression to define the criteria of the search.</param>
        IEnumerable<HotelForAdminDTO> SearchByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<HotelForAdminDTO, bool>> searchExpression);

        /// <summary>
        /// Get count of search expression for hotels by page for admin.
        /// </summary>
        /// <param name="searchExpression">Expression to define the criteria of the search.</param>
        Task<int> GetSearchCountAsync(
            Expression<Func<HotelForAdminDTO, bool>> searchExpression);
    }
}
