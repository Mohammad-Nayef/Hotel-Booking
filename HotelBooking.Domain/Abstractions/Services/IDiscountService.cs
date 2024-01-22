using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    /// <summary>
    /// Responsible for processing main operations for discount.
    /// </summary>
    public interface IDiscountService
    {
        /// <summary>
        /// Add new discount to the system.
        /// </summary>
        /// <returns>Id of the added discount.</returns>
        Task<Guid> AddAsync(DiscountDTO discount);
    }
}
