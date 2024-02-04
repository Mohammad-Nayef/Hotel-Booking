using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models.City;

namespace HotelBooking.Domain.Abstractions.Repositories.City
{
    /// <summary>
    /// Responsible for managing main storage operations for city.
    /// </summary>
    public interface ICityRepository
    {
        /// <summary>
        /// Store new city.
        /// </summary>
        /// <returns>Id of the created city.</returns>
        Task<Guid> AddAsync(CityDTO newCity);

        /// <summary>
        /// Determine whether a city exists or not.
        /// </summary>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Get total number of stored cities.
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// Delete a city by its Id.
        /// </summary>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Get a city by its Id.
        /// </summary>
        Task<CityDTO> GetByIdAsync(Guid id);

        /// <summary>
        /// Update a city.
        /// </summary>
        /// <remarks>The updated city object must contain a valid city id.</remarks>
        Task UpdateAsync(CityDTO city);

        /// <summary>
        /// Get cities with most hotels visits descending by page.
        /// </summary>
        /// <remarks>
        /// Only visits after <see cref="HotelVisitConstants.LeastRecentVisitDate"/> 
        /// are considered.
        /// </remarks>
        IEnumerable<PopularCityDTO> GetPopularCitiesByPage(
            int itemsToSkip, int itemsToTake);
    }
}
