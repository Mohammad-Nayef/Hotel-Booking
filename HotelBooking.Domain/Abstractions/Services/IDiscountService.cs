using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IDiscountService
    {
        Task<Guid> AddAsync(DiscountDTO discount);
    }
}
