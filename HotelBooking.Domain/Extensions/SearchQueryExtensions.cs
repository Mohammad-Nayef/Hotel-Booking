namespace HotelBooking.Domain.Extensions
{
    public static class SearchQueryExtensions
    {
        public static string ToComparableSearchQuery(this string query)
        {
            return query
                .ToLower()
                .Trim();
        }
    }
}
