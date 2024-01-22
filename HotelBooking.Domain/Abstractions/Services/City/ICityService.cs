using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.City;

namespace HotelBooking.Domain.Abstractions.Services.City
{
    /// <summary>
    /// Responsible for processing main operations for city.
    /// </summary>
    public interface ICityService
    {
        /// <summary>
        /// Add new city to the system.
        /// </summary>
        Task<Guid> AddAsync(CityDTO city);

        /// <summary>
        /// Determine whether a city exists in the system or not.
        /// </summary>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Get number of stored cities.
        /// </summary>
        Task<int> GetCountAsync();

        /// <summary>
        /// Delete a city from the system.
        /// </summary>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Get a city by its Id.
        /// </summary>
        Task<CityDTO> GetByIdAsync(Guid id);

        /// <summary>
        /// Update a city that exists in the system.
        /// </summary>
        /// <remarks>Id of the city must be valid.</remarks>
        Task UpdateAsync(CityDTO city);

        /// <summary>
        /// Make sure the given city Id is valid.
        /// </summary>
        Task ValidateIdAsync(Guid id);

        /// <summary>
        /// Get cities ordered descending by their popularity by page.
        /// </summary>
        Task<IEnumerable<PopularCityDTO>> GetPopularCitiesByPageAsync(PaginationDTO pagination);
    }
}