using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Domain.Abstractions.Services.Room
{
    public interface IRoomService
    {
        Task<Guid> AddAsync(RoomDTO newRoom);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCountAsync();
        Task DeleteAsync(Guid id);
        Task<RoomDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(RoomDTO room);
        Task ValidateIdAsync(Guid id);
    }
}