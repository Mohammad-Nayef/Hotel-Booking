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
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            return services;
        }
    }
}
