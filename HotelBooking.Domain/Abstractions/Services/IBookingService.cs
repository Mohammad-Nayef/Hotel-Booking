using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    /// <summary>
    /// Responsible for processing main operations for booking.
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Add new booking to the system.
        /// </summary>
        Task AddAsync(BookingDTO bookingDTO);
    }
}
