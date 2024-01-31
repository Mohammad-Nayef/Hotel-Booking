using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Entities
{
    /// <summary>
    /// Details about an image of an entity.
    /// </summary>
    /// <remarks>
    /// Height of the image must be between <see cref="ImagesConstants.MinHeight"/> and 
    /// <see cref="ImagesConstants.MaxHeight"/>. Width of the image must be between 
    /// <see cref="ImagesConstants.MinWidth"/> and <see cref="ImagesConstants.MaxWidth"/>.
    /// </remarks>
    public class Image : Entity
    {
        /// <summary>
        /// Path of the image in file system.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Id of the entity that has the image.
        /// </summary>
        public Guid EntityId { get; set; }
    }
}
