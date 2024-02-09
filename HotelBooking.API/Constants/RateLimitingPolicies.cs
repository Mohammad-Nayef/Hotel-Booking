namespace HotelBooking.Api.Constants
{
    /// <summary>
    /// Policies for specifying a rate limiting policy.
    /// </summary>
    public class RateLimitingPolicies
    {
        /// <summary>
        /// Fixed number of requests in a fixed time interval.
        /// </summary>
        public const string FixedWindowPolicy = "FixedWindow";
    }
}
