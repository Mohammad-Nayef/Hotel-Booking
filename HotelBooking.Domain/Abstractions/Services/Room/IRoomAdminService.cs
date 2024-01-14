using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Room;
using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services.Room
{
    public interface IRoomAdminService
    {
        Task<IEnumerable<RoomForAdminDTO>> GetByPageAsync(PaginationDTO pagination);
        Task<IEnumerable<RoomForAdminDTO>> SearchByPageAsync(
            PaginationDTO pagination, string searchQuery);
        Task<int> GetSearchCountAsync(string searchQuery);
        Task AddImagesAsync(Guid roomId, IEnumerable<Image> images);
    }
}
