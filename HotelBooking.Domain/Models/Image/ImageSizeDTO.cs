using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Models.Image
{
    /// <summary>
    /// Holds height and width of an image.
    /// </summary>
    public class ImageSizeDTO
    {
        /// <summary>
        /// Height of an image.
        /// </summary>
        /// <remarks>
        /// Default is <see cref="ImagesConstants.DefaultImageHeight"/>.
        /// Minimum is <see cref="ImagesConstants.MinHeight"/>.
        /// Maximum is <see cref="ImagesConstants.MaxHeight"/>.
        /// </remarks>
        public int Height { get; set; } = ImagesConstants.DefaultImageHeight;

        /// <summary>
        /// Width of an image.
        /// </summary>
        /// <remarks>
        /// Default is <see cref="ImagesConstants.DefaultImageWidth"/>.
        /// Minimum is <see cref="ImagesConstants.MinWidth"/>.
        /// Maximum is <see cref="ImagesConstants.MaxWidth"/>.
        /// </remarks>
        public int Width { get; set; } = ImagesConstants.DefaultImageWidth;
    }
}
