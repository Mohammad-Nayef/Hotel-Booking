using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services.City
{
    public interface ICityImageService
    {
        Task AddAsync(Guid cityId, IEnumerable<Image> images);
        Task<FileStream> GetImageAsync(Guid imageId);
        Task<FileStream> GetThumbnailOfImageAsync(Guid thumbnailId);
        Task<int> GetCountAsync(Guid cityId);
    }
}
