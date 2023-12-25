using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IHotelReviewService
    {
        Task AddAsync(HotelReviewDTO newReview);
    }
}
