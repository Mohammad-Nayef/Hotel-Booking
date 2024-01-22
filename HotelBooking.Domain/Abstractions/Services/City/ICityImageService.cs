using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services.City
{
    /// <summary>
    /// Responsible for processing operations for city image.
    /// </summary>
    public interface ICityImageService
    {
        /// <summary>
        /// Add list of images for a city to the system
        /// </summary>
        Task AddAsync(Guid cityId, IEnumerable<Image> images);

        /// <summary>
        /// Get file stream for a city image.
        /// </summary>
        Task<FileStream> GetImageAsync(Guid imageId);

        /// <summary>
        /// Get file stream for a city thumbnail.
        /// </summary>
        Task<FileStream> GetThumbnailOfImageAsync(Guid thumbnailId);

        /// <summary>
        /// Get number of images for a city.
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(Guid cityId);
    }
}
