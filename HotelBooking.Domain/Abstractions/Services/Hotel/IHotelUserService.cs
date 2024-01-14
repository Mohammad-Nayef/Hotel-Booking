using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Domain.Abstractions.Services.Hotel
{
    public interface IHotelUserService
    {
        Task<IEnumerable<FeaturedHotelDTO>> GetFeaturedHotelsByPageAsync(PaginationDTO pagination);
        Task<IEnumerable<HotelForUserDTO>> SearchByPageAsync(
            HotelSearchDTO hotelSearch, PaginationDTO pagination);
        int GetSearchCount(HotelSearchDTO hotelSearch);
        int GetFeaturedHotelsCount();
        Task<HotelPageDTO> GetHotelPageAsync(Guid hotelId);
        Task<IEnumerable<ReviewForHotelPageDTO>> GetReviewsByPageAsync(
            Guid id, PaginationDTO pagination);
        Task<int> GetReviewsCountAsync(Guid id);
    }
}
