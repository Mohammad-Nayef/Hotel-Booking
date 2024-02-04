using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Responsible for main operations for booking storage.
    /// </summary>
    public interface IBookingRepository
    {
        /// <summary>
        /// Store new booking.
        /// </summary>
        Task AddAsync(BookingDTO newBooking);

        /// <summary>
        /// Determine whether a room is booked in the given interval or not.
        /// </summary>
        /// <param name="roomId">Id of the room to check for booking.</param>
        /// <param name="startingDate">Starting of the interval.</param>
        /// <param name="endingDate">Ending of the interval.</param>
        bool RoomIsBookedBetween(
            Guid roomId, DateTime startingDate, DateTime endingDate);
    }
}
