using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories.Hotel
{
    public interface IHotelDiscountRepository
    {
        IEnumerable<FeaturedHotelDTO> GetHotelsWithActiveDiscountsByPage(
            int itemsToSkip, int itemsToTake);
        int GetHotelsWithActiveDiscountsCount();
    }
}
