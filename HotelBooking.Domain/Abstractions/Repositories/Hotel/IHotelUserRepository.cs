using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Domain.Abstractions.Repositories.Hotel
{
    public interface IHotelUserRepository
    {
        int GetSearchForUserCount(HotelSearchDTO hotelSearch);
        HotelPageDTO GetHotelPage(Guid id);
        IEnumerable<HotelForUserDTO> SearchForUserByPage(
            HotelSearchDTO hotelSearch, int itemsToSkip, int itemsToTake);
        IEnumerable<RoomForUserDTO> GetAvailableRooms(
            Guid id, int itemsToSkip, int itemsToTake);
        int GetAvailableRoomsCount(Guid id);
    }
}
