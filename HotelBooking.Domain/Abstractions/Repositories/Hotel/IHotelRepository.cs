using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Domain.Abstractions.Repositories.Hotel
{
    /// <summary>
    /// Responsible for main storage operations for hotel.
    /// </summary>
    public interface IHotelRepository
    {
        /// <summary>
        /// Store new hotel.
        /// </summary>
        /// <returns>Id of the created hotel.</returns>
        Task<Guid> AddAsync(HotelDTO newHotel);

        /// <summary>
        /// Determine whether an hotel exists or not.
        /// </summary>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Get total number of stored hotels.
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// Delete an hotel by its Id.
        /// </summary>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Get an hotel by its Id.
        /// </summary>
        Task<HotelDTO> GetByIdAsync(Guid id);

        /// <summary>
        /// Update an hotel.
        /// </summary>
        /// <remarks>The updated hotel object must contain a valid hotel id.</remarks>
        Task UpdateAsync(HotelDTO hotel);
    }
}
