namespace HotelBooking.Infrastructure.Extensions
{
    internal static class DecimalExtensions
    {
        public static bool IsBetweenInclusive(this decimal value, decimal min, decimal max) =>
            value >= min && value <= max;
    }
}
