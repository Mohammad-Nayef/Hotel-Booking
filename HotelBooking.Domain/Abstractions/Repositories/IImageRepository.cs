using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface IImageRepository
    {
        Task AddRangeAsync(Guid entityId, IEnumerable<Image> images);
        Task<bool> ExistsAsync(Guid imageId);
        FileStream Get(Guid imageId);
        FileStream GetThumbnail(Guid thumbnailId);
        Task<bool> ThumbnailExistsAsync(Guid thumbnailId);
        Task<int> GetCountAsync(Guid entityId);
    }
}
