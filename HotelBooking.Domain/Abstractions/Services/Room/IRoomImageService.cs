using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services.Room
{
    public interface IRoomImageService
    {
        Task AddAsync(Guid roomId, IEnumerable<Image> images);
        Task<int> GetCountAsync(Guid roomId);
        Task<FileStream> GetImageAsync(Guid imageId);
        Task<FileStream> GetThumbnailOfImageAsync(Guid thumbnailId);
    }
}
