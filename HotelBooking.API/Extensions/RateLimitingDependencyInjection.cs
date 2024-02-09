using System.Threading.RateLimiting;
using HotelBooking.Api.Constants;
using Microsoft.AspNetCore.RateLimiting;

namespace HotelBooking.Api.Extensions
{
    internal static class RateLimitingDependencyInjection
    {
        /// <summary>
        /// Register a middleware for adding a rate limiter.
        /// </summary>
        public static IServiceCollection AddRateLimitingService(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddFixedWindowLimiter(RateLimitingPolicies.FixedWindowPolicy, options =>
                {
                    options.PermitLimit = 10;
                    options.Window = TimeSpan.FromSeconds(2);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 5;
                });
            });

            return services;
        }
    }
}
