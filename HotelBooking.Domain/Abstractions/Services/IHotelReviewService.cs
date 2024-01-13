using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IHotelReviewService
    {
        Task AddAsync(HotelReviewDTO newReview);
        IEnumerable<ReviewForHotelPageDTO> GetReviewsByHotelByPage(
            Guid hotelId, PaginationDTO pagination);
        Task<int> GetReviewsByHotelCountAsync(Guid hotelId);
    }
}
