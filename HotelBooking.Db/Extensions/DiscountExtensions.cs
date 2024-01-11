using HotelBooking.Db.Tables;

namespace HotelBooking.Db.Extensions
{
    internal static class DiscountExtensions
    {
        public static DiscountTable GetHighestActive(this List<DiscountTable> discounts) =>
            discounts
                .OrderByDescending(discount => discount.AmountPercent)
                .FirstOrDefault(discount => discount.IsActive());

        public static bool HasActiveDiscount(this List<DiscountTable> discounts) =>
            discounts.Exists(discount => discount.IsActive());

        public static bool IsActive(this DiscountTable discount) =>
            discount.StartingDate <= DateTime.UtcNow &&
            discount.EndingDate > DateTime.UtcNow;
    }
}
