using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.City;
using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services.City
{
    /// <summary>
    /// Responsible for processing operations for city by admin.
    /// </summary>
    public interface ICityAdminService
    {
        /// <summary>
        /// Add a list of images for a city to the system .
        /// </summary>
        Task AddImagesAsync(Guid cityId, IEnumerable<Image> images);

        /// <summary>
        /// Get list of cities to view for admin by page.
        /// </summary>
        Task<IEnumerable<CityForAdminDTO>> GetByPageAsync(PaginationDTO pagination);

        /// <summary>
        /// Search for cities for admin by page.
        /// </summary>
        /// <param name="searchQuery">Textual content of the search query.</param>
        Task<IEnumerable<CityForAdminDTO>> SearchByPageAsync(
            PaginationDTO pagination, string searchQuery);

        /// <summary>
        /// Number of cities for result of search query for admin.
        /// </summary>
        /// <param name="searchQuery">Textual content of the search query.</param>
        Task<int> GetSearchCountAsync(string searchQuery);
    }
}