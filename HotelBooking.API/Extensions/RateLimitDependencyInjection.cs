using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace HotelBooking.Api.Extensions
{
    public static class RateLimitDependencyInjection
    {
        public static IServiceCollection AddRateLimitingService(
            this IServiceCollection services, string fixedWindowPolicy)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.AddFixedWindowLimiter(fixedWindowPolicy, options =>
                {
                    options.PermitLimit = 10;
                    options.Window = TimeSpan.FromSeconds(5);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 5;

                });
            });

            return services;
        }
    }
}
