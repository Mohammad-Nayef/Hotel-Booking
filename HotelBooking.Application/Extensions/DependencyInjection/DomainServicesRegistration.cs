using HotelBooking.Application.Services;
using HotelBooking.Domain.Abstractions.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.Application.Extensions.DependencyInjection
{
    public static class DomainServicesRegistration
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
