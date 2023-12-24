using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface IBookingRepository
    {
        Task AddAsync(BookingDTO newBooking);
        bool RoomIsBookedBetween(
            Guid roomId, DateTime startingDate, DateTime endingDate);
    }
}
