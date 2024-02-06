using HotelBooking.Domain.Configurations;

namespace HotelBooking.Api.Extensions
{
    public static class ConfigurationsExtensions
    {
        /// <summary>
        /// Bind configurations from appsettings.json to their corresponding classes.
        /// </summary>
        public static IServiceCollection BindConfigurations(
            this IServiceCollection services, IConfiguration configurations)
        {
            services.Configure<EmailConfigurations>(
                configurations.GetSection(nameof(EmailConfigurations)));
            services.Configure<JwtConfigurations>(
                configurations.GetSection(nameof(JwtConfigurations)));
            services.Configure<ConnectionStrings>(
                configurations.GetSection(nameof(ConnectionStrings)));

            return services;
        }
    }
}
