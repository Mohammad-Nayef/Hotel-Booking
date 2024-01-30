using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IImageService
    {
        /// <summary>
        /// Add list of images for an entity to the system
        /// </summary>
        Task AddAsync(Guid entityId, IEnumerable<Image> images);

        /// <summary>
        /// Get file stream of an entity image.
        /// </summary>
        Task<FileStream> GetImageAsync(Guid imageId);

        /// <summary>
        /// Get file stream for an entity thumbnail.
        /// </summary>
        Task<FileStream> GetThumbnailOfImageAsync(Guid thumbnailId);

        /// <summary>
        /// Get number of images for an entity.
        /// </summary>
        Task<int> GetCountAsync(Guid entityId);

        /// <summary>
        /// Get list of images IDs for an entity.
        /// </summary>
        Task<IEnumerable<Guid>> GetImagesIdsAsync(Guid entityId);
    }
}
