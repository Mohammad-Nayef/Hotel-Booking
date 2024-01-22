using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Responsible for managing main operations for cart item storage.
    /// </summary>
    public interface ICartItemRepository
    {
        /// <summary>
        /// Store new cart item.
        /// </summary>
        /// <returns>Id of the new cart item.</returns>
        Task<Guid> AddAsync(CartItemDTO newCartItem);

        /// <summary>
        /// Determine whether a room is in a user's cart items or not.
        /// </summary>
        Task<bool> ExistsByUserAndRoomAsync(Guid userId, Guid roomId);

        /// <summary>
        /// Get a list of cart items for a user by page.
        /// </summary>
        IEnumerable<CartItemDTO> GetAllForUserByPage(
            Guid userId, int itemsToSkip, int itemsToTake);

        /// <summary>
        /// Get number of cart items for a user.
        /// </summary>
        Task<int> GetCountForUserAsync(Guid userId);
    }
}
