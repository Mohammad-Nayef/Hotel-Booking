using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface ICartItemRepository
    {
        Task<Guid> AddAsync(CartItemDTO newCartItem);
        bool ExistsByUserAndRoomAsync(Guid userId, Guid roomId);
        IEnumerable<CartItemDTO> GetAllForUserByPage(
            Guid userId, int itemsToSkip, int itemsToTake);
        int GetCountForUser(Guid userId);
    }
}
