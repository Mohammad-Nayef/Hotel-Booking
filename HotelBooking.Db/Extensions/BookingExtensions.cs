using HotelBooking.Db.Tables;

namespace HotelBooking.Db.Extensions
{
    internal static class BookingExtensions
    {
        public static bool IntersectsWith(
            this BookingTable booking, DateTime startingDate, DateTime endingDate)
        {
            return startingDate.IsBetween(booking.StartingDate, booking.EndingDate) ||
                endingDate.IsBetween(booking.StartingDate, booking.EndingDate);
        }

        public static bool IntersectsWith(this BookingTable booking, DateTime date) =>
            date.IsBetween(booking.StartingDate, booking.EndingDate);

        private static bool IsBetween(
            this DateTime dateToTest, DateTime startingDate, DateTime endingDate)
        {
            return dateToTest >= startingDate && dateToTest <= endingDate;
        }
    }
}
