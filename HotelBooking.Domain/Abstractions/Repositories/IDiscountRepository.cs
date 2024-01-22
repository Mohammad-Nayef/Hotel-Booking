using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Responsible for managing main operations for discount storage.
    /// </summary>
    public interface IDiscountRepository
    {
        /// <summary>
        /// Store new discount.
        /// </summary>
        /// <returns>Id of the stored discount.</returns>
        Task<Guid> AddAsync(DiscountDTO newDiscount);
    }
}
