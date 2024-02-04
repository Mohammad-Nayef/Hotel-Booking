using System.Linq.Expressions;
using HotelBooking.Domain.Models.City;

namespace HotelBooking.Domain.Abstractions.Repositories.City
{
    /// <summary>
    /// Responsible for managing storage for city by admin.
    /// </summary>
    public interface ICityAdminRepository
    {
        /// <summary>
        /// Get a list of cities by page.
        /// </summary>
        IEnumerable<CityForAdminDTO> GetByPage(int itemsToSkip, int itemsToTake);

        /// <summary>
        /// Search for cities by page for admin.
        /// </summary>
        /// <param name="searchExpression">Expression to define the criteria of the search.</param>
        IEnumerable<CityForAdminDTO> SearchByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<CityForAdminDTO, bool>> searchExpression);

        /// <summary>
        /// Get count of search expression for cities by page for admin.
        /// </summary>
        /// <param name="searchExpression">Expression to define the criteria of the search.</param>
        Task<int> GetSearchCountAsync(
            Expression<Func<CityForAdminDTO, bool>> searchExpression);
    }
}
