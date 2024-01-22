namespace HotelBooking.Infrastructure.Extensions
{
    internal static class DecimalExtensions
    {
        /// <summary>
        /// Determine whether a decimal value is between 2 other values.
        /// </summary>
        public static bool IsBetweenInclusive(this decimal value, decimal min, decimal max) =>
            value >= min && value <= max;
    }
}
