using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Extensions
{
    internal static class BookingExtensions
    {
        /// <summary>
        /// Determine whether a booking intersects with a given interval.
        /// </summary>
        /// <param name="booking">Booking to check for intersecting.</param>
        /// <param name="startingDate">Starting of the interval.</param>
        /// <param name="endingDate">Ending of the interval.</param>
        public static bool IntersectsWith(
            this BookingTable booking, DateTime startingDate, DateTime endingDate)
        {
            return startingDate.IsBetween(booking.StartingDate, booking.EndingDate) ||
                endingDate.IsBetween(booking.StartingDate, booking.EndingDate);
        }

        /// <summary>
        /// Determine whether a booking intersects with a given date.
        /// </summary>
        /// <param name="booking">Booking to check for intersecting.</param>
        /// <param name="date">Date to check for its intersecting with the booking.</param>
        public static bool IntersectsWith(this BookingTable booking, DateTime date) =>
            date.IsBetween(booking.StartingDate, booking.EndingDate);

        /// <summary>
        /// Determine whether a date is within a given interval or not.
        /// </summary>
        /// <param name="dateToTest">Date to check.</param>
        /// <param name="startingDate">Starting of the interval.</param>
        /// <param name="endingDate">Ending of the interval.</param>
        /// <returns></returns>
        private static bool IsBetween(
            this DateTime dateToTest, DateTime startingDate, DateTime endingDate)
        {
            return dateToTest >= startingDate && dateToTest <= endingDate;
        }
    }
}
