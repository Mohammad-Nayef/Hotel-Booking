using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Extensions
{
    internal static class DiscountExtensions
    {
        /// <summary>
        /// Get the highest active discount at the moment.
        /// </summary>
        public static DiscountTable GetHighestActive(this List<DiscountTable> discounts)
        {
            return discounts
                .Where(discount => discount.IsActive())
                .MaxBy(discount => discount.AmountPercent);
        }

        /// <summary>
        /// Determine whether a list of discounts has at least one active discount at the moment
        /// or not.
        /// </summary>
        public static bool HasActiveDiscount(this List<DiscountTable> discounts) =>
            discounts.Exists(discount => discount.IsActive());

        /// <summary>
        /// Determine whether a discount is active at the moment or not.
        /// </summary>
        public static bool IsActive(this DiscountTable discount)
        {
            return discount.StartingDate <= DateTime.UtcNow &&
                discount.EndingDate > DateTime.UtcNow;
        }
    }
}
