using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    /// <summary>
    /// Responsible for processing main operations for booking.
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Add new booking to the system and send an email for the user who created it 
        /// containing the booking's details.
        /// </summary>
        Task AddAsync(BookingDTO bookingDTO);
    }
}
