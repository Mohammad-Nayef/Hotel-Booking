using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface IHotelReviewRepository
    {
        Task AddAsync(HotelReviewDTO newReview);
        Task<bool> ExistsByUserAndHotelAsync(Guid userId, Guid hotelId);
        IEnumerable<ReviewForHotelPageDTO> GetReviewsByHotelByPage(
            Guid hotelId, int itemsToSkip, int itemsToTake);
        Task<int> GetReviewsByHotelCountAsync(Guid hotelId);
    }
}
