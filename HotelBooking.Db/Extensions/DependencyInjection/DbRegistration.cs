using HotelBooking.Db.Repositories;
using HotelBooking.Domain.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.Db.Extensions.DependencyInjection
{
    public static class DbRegistration
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<HotelsBookingDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
