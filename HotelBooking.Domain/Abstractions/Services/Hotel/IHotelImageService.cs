using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services.Hotel
{
    /// <summary>
    /// Responsible for processing operations for hotel images.
    /// </summary>
    public interface IHotelImageService
    {
        /// <summary>
        /// Add a list of images for a hotel to the system.
        /// </summary>
        Task AddAsync(Guid hotelId, IEnumerable<Image> images);

        /// <summary>
        /// Get file stream for a hotel image.
        /// </summary>
        Task<FileStream> GetImageAsync(Guid imageId);

        /// <summary>
        /// Get file stream for a hotel thumbnail.
        /// </summary>
        Task<FileStream> GetThumbnailOfImageAsync(Guid thumbnailId);

        /// <summary>
        /// Get number of images for a hotel.
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(Guid hotelId);
    }
}
