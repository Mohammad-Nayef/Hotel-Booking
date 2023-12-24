using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface ICartItemRepository
    {
        Task<Guid> AddAsync(CartItemDTO newCartItem);
        bool ExistsByUserAndRoomAsync(Guid userId, Guid roomId);
    }
}
