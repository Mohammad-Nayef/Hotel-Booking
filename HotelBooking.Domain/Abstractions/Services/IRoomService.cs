using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IRoomService
    {
        Task<Guid> AddAsync(RoomDTO newRoom);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCountAsync();
        Task<IEnumerable<RoomForAdminDTO>> GetForAdminByPageAsync(PaginationDTO pagination);
        Task DeleteAsync(Guid id);
        Task<RoomDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(RoomDTO room);
        Task<IEnumerable<RoomForAdminDTO>> SearchByRoomForAdminByPageAsync(
            PaginationDTO pagination, string searchQuery);
        Task<int> GetSearchByRoomForAdminCountAsync(string searchQuery);
    }
}