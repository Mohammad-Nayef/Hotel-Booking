using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Entities
{
    /// <summary>
    /// Details about an image that can be for an hotel, room or a city.
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
        /// Path of the thumbnail of the image in file system.
        /// </summary>
        public string ThumbnailPath { get; set; }

        public Hotel? Hotel { get; set; }

        public Room? Room { get; set; }

        public City? City { get; set; }
    }
}
