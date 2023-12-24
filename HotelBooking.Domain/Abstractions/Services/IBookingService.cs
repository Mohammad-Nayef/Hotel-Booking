using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IBookingService
    {
        Task AddAsync(BookingDTO bookingDTO);
    }
}
