using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface IDiscountRepository
    {
        Task<Guid> AddAsync(DiscountDTO discount);
    }
}
