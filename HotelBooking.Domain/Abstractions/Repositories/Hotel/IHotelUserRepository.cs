using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Domain.Abstractions.Repositories.Hotel
{
    public interface IHotelUserRepository
    {
        int GetSearchForUserCount(HotelSearchDTO hotelSearch);
        Task<HotelPageDTO> GetHotelPageAsync(Guid id);
        IEnumerable<HotelForUserDTO> SearchForUserByPage(
            HotelSearchDTO hotelSearch, int itemsToSkip, int itemsToTake);
        IEnumerable<RoomForUserDTO> GetAvailableRooms(
            Guid id, int itemsToSkip, int itemsToTake);
        int GetAvailableRoomsCount(Guid id);
        IEnumerable<VisitedHotelDTO> GetRecentlyVisitedByPage(
            Guid userId, int itemsToSkip, int itemsToTake);
        Task<int> GetVisitedCountAsync(Guid userId);
    }
}
