using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Domain.Abstractions.Repositories.Hotel
{
    /// <summary>
    /// Responsible for managing storage for hotel discounts.
    /// </summary>
    public interface IHotelDiscountRepository
    {
        /// <summary>
        /// Get the highest active discount for an hotel at the moment by hotel Id.
        /// </summary>
        DiscountDTO GetHighestActiveDiscount(Guid hotelId);

        /// <summary>
        /// Get list of hotels with at least one active discount at the moment by page.
        /// </summary>
        IEnumerable<FeaturedHotelDTO> GetHotelsWithActiveDiscountsByPage(
            int itemsToSkip, int itemsToTake);

        /// <summary>
        /// Get number of hotels with at least one active discount at the moment.
        /// </summary>
        int GetHotelsWithActiveDiscountsCount();
    }
}
