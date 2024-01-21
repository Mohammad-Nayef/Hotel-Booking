using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface ICartItemService
    {
        Task<Guid> AddAsync(CartItemDTO newCartItem);
        Task<IEnumerable<CartItemDTO>> GetAllForUserByPageAsync(Guid userId, PaginationDTO pagination);
        Task<int> GetCountForUserAsync(Guid userId);
    }
}
