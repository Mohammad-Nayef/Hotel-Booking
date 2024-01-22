using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;
using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services.Hotel
{
    /// <summary>
    /// Responsible for processing operations for hotels and rooms by admin.
    /// </summary>
    public interface IHotelAdminService
    {
        /// <summary>
        /// Add a list of images for a hotel to the system.
        /// </summary>
        Task AddImagesAsync(Guid hotelId, IEnumerable<Image> images);

        /// <summary>
        /// Get list of hotels to view for admin by page.
        /// </summary>
        Task<IEnumerable<HotelForAdminDTO>> GetByPageAsync(PaginationDTO pagination);

        /// <summary>
        /// Search for hotels for admin by page.
        /// </summary>
        /// <param name="searchQuery">Textual content of the search query.</param>
        Task<IEnumerable<HotelForAdminDTO>> SearchByPageAsync(
            PaginationDTO pagination, string searchQuery);

        /// <summary>
        /// Number of hotels for result of search query for admin.
        /// </summary>
        /// <param name="searchQuery">Textual content of the search query.</param>
        Task<int> GetSearchCountAsync(string searchQuery);
    }
}
