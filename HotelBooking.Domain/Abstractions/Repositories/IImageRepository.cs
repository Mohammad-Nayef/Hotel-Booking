using HotelBooking.Domain.Models.Image;
using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Responsible for managing storage of images.
    /// </summary>
    public interface IImageRepository
    {
        /// <summary>
        /// Store list of images for an entity.
        /// </summary>
        /// <param name="entityId">Id of the entity to add images for.</param>
        /// <param name="images">Images to store.</param>
        Task AddRangeAsync(Guid entityId, IEnumerable<Image> images);

        /// <summary>
        /// Determine whether the image exists or not.
        /// </summary>
        /// <param name="imageId">Id of the image to check for existence.</param>
        Task<bool> ExistsAsync(Guid imageId);

        /// <summary>
        /// Get a file stream of an image by its Id.
        /// </summary>
        /// <param name="imageId">Id of the image to get.</param>
        Task<Stream> GetAsync(Guid imageId, ImageSizeDTO imageSize);

        /// <summary>
        /// Get number of images for an entity by its Id.
        /// </summary>
        /// <param name="entityId">Id of the entity.</param>
        /// <returns></returns>
        Task<int> GetCountAsync(Guid entityId);

        /// <summary>
        /// Get list of images IDs for an entity.
        /// </summary>
        IEnumerable<Guid> GetImagesIds(Guid entityId);
    }
}
