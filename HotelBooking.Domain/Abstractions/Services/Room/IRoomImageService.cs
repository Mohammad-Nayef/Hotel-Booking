using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services.Room
{
    /// <summary>
    /// Responsible for processing operations for room images.
    /// </summary>
    public interface IRoomImageService
    {
        /// <summary>
        /// Add a list of images for a room to the system.
        /// </summary>
        Task AddAsync(Guid roomId, IEnumerable<Image> images);

        /// <summary>
        /// Get file stream for a room image.
        /// </summary>
        Task<FileStream> GetImageAsync(Guid imageId);

        /// <summary>
        /// Get file stream for a room thumbnail.
        /// </summary>
        Task<FileStream> GetThumbnailOfImageAsync(Guid thumbnailId);

        /// <summary>
        /// Get number of images for a room.
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(Guid roomId);
    }
}
