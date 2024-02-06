using SixLabors.ImageSharp;

namespace HotelBooking.Api.Extensions
{
    internal static class ImagesExtensions
    {
        /// <summary>
        /// Get list of images from list of files.
        /// </summary>
        public static IEnumerable<Image> ToImages(this List<IFormFile> imagesForms) =>
            imagesForms.Select(form => form.ToImage());

        private static Image ToImage(this IFormFile file) => Image.Load(file.OpenReadStream());
    }
}
