using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services.Hotel
{
    public interface IHotelImageService
    {
        Task AddAsync(Guid hotelId, IEnumerable<Image> images);
        Task<int> GetCountAsync(Guid hotelId);
        Task<FileStream> GetImageAsync(Guid imageId);
        Task<FileStream> GetThumbnailOfImageAsync(Guid thumbnailId);
    }
}
