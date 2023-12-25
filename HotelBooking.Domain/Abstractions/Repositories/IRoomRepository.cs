using System.Linq.Expressions;
using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface IRoomRepository
    {
        Task<Guid> AddAsync(RoomDTO newRoom);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCountAsync();
        IEnumerable<RoomForAdminDTO> GetForAdminByPage(int itemsToSkip, int itemsToTake);
        Task DeleteAsync(Guid id);
        Task<RoomDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(RoomDTO room);
        IEnumerable<RoomForAdminDTO> SearchByRoomForAdminByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<RoomForAdminDTO, bool>> searchExpression);
        Task<int> GetSearchByRoomForAdminCountAsync(
            Expression<Func<RoomForAdminDTO, bool>> searchExpression);
    }
}
