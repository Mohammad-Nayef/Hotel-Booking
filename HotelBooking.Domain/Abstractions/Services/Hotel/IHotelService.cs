using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Domain.Abstractions.Services.Hotel
{
    /// <summary>
    /// Responsible for processing main operations for hotels.
    /// </summary>
    public interface IHotelService
    {
        /// <summary>
        /// Add a new hotel to the system.
        /// </summary>
        Task<Guid> AddAsync(HotelDTO hotel);

        /// <summary>
        /// Determine whether a hotel exists in the system or not.
        /// </summary>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Get the number of stored hotels.
        /// </summary>
        Task<int> GetCountAsync();

        /// <summary>
        /// Delete a hotel from the system.
        /// </summary>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Get a hotel by its Id.
        /// </summary>
        Task<HotelDTO> GetByIdAsync(Guid id);

        /// <summary>
        /// Update a hotel that exists in the system.
        /// </summary>
        /// <remarks>Id of the hotel must be valid.</remarks>
        Task UpdateAsync(HotelDTO hotel);

        /// <summary>
        /// Make sure the given hotel Id is valid.
        /// </summary>
        Task ValidateIdAsync(Guid id);
    }
}
