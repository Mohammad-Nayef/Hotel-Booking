using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface IImageRepository
    {
        Task AddForCityAsync(Guid cityId, IEnumerable<Image> images);
        Task AddForHotelAsync(Guid hotelId, IEnumerable<Image> images);
        Task AddForRoomAsync(Guid roomId, IEnumerable<Image> images);
        Task<bool> HotelImageExistsAsync(Guid imageId);
        Task<bool> CityImageExistsAsync(Guid imageId);
        Task<bool> RoomImageExistsAsync(Guid imageId);
        FileStream GetRoomImage(Guid imageId);
        FileStream GetHotelImage(Guid imageId);
        FileStream GetCityImage(Guid imageId);
        FileStream GetCityThumbnail(Guid thumbnailId);
        Task<bool> CityThumbnailExistsAsync(Guid thumbnailId);
        FileStream GetHotelThumbnail(Guid thumbnailId);
        Task<bool> HotelThumbnailExistsAsync(Guid thumbnailId);
        FileStream GetRoomThumbnail(Guid thumbnailId);
        Task<bool> RoomThumbnailExistsAsync(Guid thumbnailId);
    }
}
