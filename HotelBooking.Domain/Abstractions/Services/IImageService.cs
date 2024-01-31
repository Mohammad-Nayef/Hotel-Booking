using HotelBooking.Domain.Models.Image;
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
        Task<Stream> GetImageAsync(Guid imageId, ImageSizeDTO imageSize);

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
