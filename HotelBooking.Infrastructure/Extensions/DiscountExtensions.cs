using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Extensions
{
    internal static class DiscountExtensions
    {
        public static DiscountTable GetHighestActive(this List<DiscountTable> discounts)
        {
            return discounts
                .Where(discount => discount.IsActive())
                .MaxBy(discount => discount.AmountPercent);
        }

        public static bool HasActiveDiscount(this List<DiscountTable> discounts) =>
            discounts.Exists(discount => discount.IsActive());

        public static bool IsActive(this DiscountTable discount)
        {
            return discount.StartingDate <= DateTime.UtcNow &&
                discount.EndingDate > DateTime.UtcNow;
        }
    }
}
