using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface IRoomRepository
    {
        Task<Guid> AddAsync(RoomDTO newRoom);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCountAsync();
        IEnumerable<RoomForAdminDTO> GetForAdminByPage(
            int itemsToSkip, int itemsToTake);
        Task DeleteAsync(Guid id);
    }
}
