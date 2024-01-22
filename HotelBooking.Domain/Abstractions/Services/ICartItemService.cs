using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    /// <summary>
    /// Responsible for processing main operations for cart item.
    /// </summary>
    public interface ICartItemService
    {
        /// <summary>
        /// Add new cart item to the system.
        /// </summary>
        /// <returns>Id of the added cart item.</returns>
        Task<Guid> AddAsync(CartItemDTO newCartItem);

        /// <summary>
        /// Get list of cart items for a user by page.
        /// </summary>
        Task<IEnumerable<CartItemDTO>> GetAllForUserByPageAsync(
            Guid userId, PaginationDTO pagination);

        /// <summary>
        /// Get number of cart items for a user.
        /// </summary>
        Task<int> GetCountForUserAsync(Guid userId);
    }
}
