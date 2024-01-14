using System.Linq.Expressions;
using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Domain.Abstractions.Repositories.Room
{
    public interface IRoomRepository
    {
        Task<Guid> AddAsync(RoomDTO newRoom);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCountAsync();
        Task DeleteAsync(Guid id);
        Task<RoomDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(RoomDTO room);
    }
}
