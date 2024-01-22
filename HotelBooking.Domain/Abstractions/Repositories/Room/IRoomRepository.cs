using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Domain.Abstractions.Repositories.Room
{
    /// <summary>
    /// Responsible for main storage operations for rooms.
    /// </summary>
    public interface IRoomRepository
    {
        /// <summary>
        /// Store new room.
        /// </summary>
        /// <returns>Id of the created room.</returns>
        Task<Guid> AddAsync(RoomDTO newRoom);

        /// <summary>
        /// Determine whether a room exists or not.
        /// </summary>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Get total number of stored rooms.
        /// </summary>
        Task<int> GetCountAsync();

        /// <summary>
        /// Delete a room by its Id.
        /// </summary>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Get a room by its Id.
        /// </summary>
        Task<RoomDTO> GetByIdAsync(Guid id);

        /// <summary>
        /// Update a room.
        /// </summary>
        /// <remarks>The updated room object must contain a valid room id.</remarks>
        Task UpdateAsync(RoomDTO room);
    }
}
