using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IImageService
    {
        Task AddForCityAsync(Guid cityId, IEnumerable<Image> images);
        Task AddForHotelAsync(Guid hotelId, IEnumerable<Image> images);
        Task AddForRoomAsync(Guid roomId, IEnumerable<Image> images);
        Task<FileStream> GetCityImageAsync(Guid imageId);
        Task<FileStream> GetHotelImageAsync(Guid imageId);
        Task<FileStream> GetRoomImageAsync(Guid imageId);
    }
}
