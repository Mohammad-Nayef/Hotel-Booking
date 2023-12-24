using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface ICartItemService
    {
        Task<Guid> AddAsync(CartItemDTO newCartItem);
    }
}
