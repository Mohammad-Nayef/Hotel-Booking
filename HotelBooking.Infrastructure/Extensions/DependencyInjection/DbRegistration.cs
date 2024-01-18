using HotelBooking.Db.Repositories.Hotel;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Repositories.City;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Abstractions.Repositories.Room;
using HotelBooking.Infrastructure.Repositories;
using HotelBooking.Infrastructure.Repositories.City;
using HotelBooking.Infrastructure.Repositories.Hotel;
using HotelBooking.Infrastructure.Repositories.Room;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.Infrastructure.Extensions.DependencyInjection
{
    public static class DbRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<HotelsBookingDbContext>();
            AddHotel(services);
            AddCity(services);
            AddRoom(services);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHotelReviewRepository, HotelReviewRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            return services;
        }

        private static void AddRoom(IServiceCollection services)
        {
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomImageRepository, RoomImageRepository>();
            services.AddScoped<IRoomAdminRepository, RoomAdminRepository>();
        }

        private static void AddCity(IServiceCollection services)
        {
            services.AddScoped<ICityAdminRepository, CityAdminRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICityImageRepository, CityImageRepository>();
        }

        private static void AddHotel(IServiceCollection services)
        {
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IHotelUserRepository, HotelUserRepository>();
            services.AddScoped<IHotelImageRepository, HotelImageRepository>();
            services.AddScoped<IHotelAdminRepository, HotelAdminRepository>();
            services.AddScoped<IHotelDiscountRepository, HotelDiscountRepository>();
            services.AddScoped<IHotelVisitRepository, HotelVisitRepository>();
        }
    }
}
