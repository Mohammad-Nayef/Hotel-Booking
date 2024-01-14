using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Domain.Abstractions.Repositories.Hotel
{
    public interface IHotelUserRepository
    {
        int GetSearchForUserCount(HotelSearchDTO hotelSearch);
        HotelPageDTO GetHotelPage(Guid id);
        IEnumerable<HotelForUserDTO> SearchForUserByPage(
            HotelSearchDTO hotelSearch, int itemsToSkip, int itemsToTake);
    }
}
