using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Domain.Abstractions.Services.Room
{
    /// <summary>
    /// Responsible for processing main operations for rooms.
    /// </summary>
    public interface IRoomService
    {
        /// <summary>
        /// Add a new room to the system.
        /// </summary>
        Task<Guid> AddAsync(RoomDTO room);

        /// <summary>
        /// Determine whether a room exists in the system or not.
        /// </summary>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Get the number of stored rooms.
        /// </summary>
        Task<int> GetCountAsync();

        /// <summary>
        /// Delete a room from the system.
        /// </summary>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Get a room by its Id.
        /// </summary>
        Task<RoomDTO> GetByIdAsync(Guid id);

        /// <summary>
        /// Update a room that exists in the system.
        /// </summary>
        /// <remarks>Id of the room must be valid.</remarks>
        Task UpdateAsync(RoomDTO room);

        /// <summary>
        /// Make sure the given room Id is valid.
        /// </summary>
        Task ValidateIdAsync(Guid id);
    }
}