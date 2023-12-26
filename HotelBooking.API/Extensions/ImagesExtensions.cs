using SixLabors.ImageSharp;

namespace HotelBooking.Api.Extensions
{
    public static class ImagesExtensions
    {
        public static IEnumerable<Image> ToImages(this List<IFormFile> imagesForms) =>
            imagesForms.Select(form => form.ToImage());

        private static Image ToImage(this IFormFile file) => Image.Load(file.OpenReadStream());
    }
}
