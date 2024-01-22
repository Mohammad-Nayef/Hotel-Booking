using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Room;
using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services.Room
{
    /// <summary>
    /// Responsible for processing operations for rooms by admin.
    /// </summary>
    public interface IRoomAdminService
    {
        /// <summary>
        /// Add a list of images for a room to the system.
        /// </summary>
        Task AddImagesAsync(Guid roomId, IEnumerable<Image> images);

        /// <summary>
        /// Get list of rooms to view for admin by page.
        /// </summary>
        Task<IEnumerable<RoomForAdminDTO>> GetByPageAsync(PaginationDTO pagination);

        /// <summary>
        /// Search for rooms for admin by page.
        /// </summary>
        /// <param name="searchQuery">Textual content of the search query.</param>
        Task<IEnumerable<RoomForAdminDTO>> SearchByPageAsync(
            PaginationDTO pagination, string searchQuery);

        /// <summary>
        /// Number of rooms for result of search query for admin.
        /// </summary>
        /// <param name="searchQuery">Textual content of the search query.</param>
        Task<int> GetSearchCountAsync(string searchQuery);
    }
}
