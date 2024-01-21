using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Domain.Abstractions.Services.Hotel
{
    public interface IHotelUserService
    {
        Task<IEnumerable<FeaturedHotelDTO>> GetFeaturedHotelsByPageAsync(PaginationDTO pagination);
        int GetFeaturedHotelsCount();
        Task<IEnumerable<HotelForUserDTO>> SearchByPageAsync(
            HotelSearchDTO hotelSearch, PaginationDTO pagination);
        int GetSearchCount(HotelSearchDTO hotelSearch);
        Task<HotelPageDTO> GetHotelPageAsync(Guid hotelId, Guid userId);
        Task<IEnumerable<ReviewForHotelPageDTO>> GetReviewsByPageAsync(
            Guid id, PaginationDTO pagination);
        Task<int> GetReviewsCountAsync(Guid id);
        Task<IEnumerable<RoomForUserDTO>> GetAvailableRoomsAsync(
            Guid id, PaginationDTO pagination);
        int GetAvailableRoomsCount(Guid id);
        Task<IEnumerable<VisitedHotelDTO>> GetRecentlyVisitedByPageAsync(
            Guid userId, PaginationDTO pagination);
        Task<int> GetRecentlyVisitedCountAsync(Guid userId);
    }
}
