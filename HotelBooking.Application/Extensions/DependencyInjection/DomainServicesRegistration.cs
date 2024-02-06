using System.Text;
using FluentValidation;
using HotelBooking.Application.Services;
using HotelBooking.Application.Services.City;
using HotelBooking.Application.Services.Hotel;
using HotelBooking.Application.Services.Room;
using HotelBooking.Application.Validators;
using HotelBooking.Application.Validators.Hotel;
using HotelBooking.Application.Validators.Image;
using HotelBooking.Application.Validators.User;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Abstractions.Services.City;
using HotelBooking.Domain.Abstractions.Services.Hotel;
using HotelBooking.Domain.Abstractions.Services.Room;
using HotelBooking.Domain.Configurations;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.City;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Domain.Models.Image;
using HotelBooking.Domain.Models.Room;
using HotelBooking.Domain.Models.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Extensions.DependencyInjection
{
    public static class DomainServicesRegistration
    {
        /// <summary>
        /// Register all the services of the domain.
        /// </summary>
        public static IServiceCollection AddDomainServices(
            this IServiceCollection services, IConfiguration config)
        {
            AddAuthentication(services, config);
            AddValidators(services);
            AddHotel(services);
            AddCity(services);
            AddRoom(services);
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHotelReviewService, HotelReviewService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IAuthTokenProcessor, AuthTokenProcessor>();

            return services;
        }

        private static void AddRoom(IServiceCollection services)
        {
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomAdminService, RoomAdminService>();
            services.AddScoped<IRoomImageService, RoomImageService>();
        }

        private static void AddCity(IServiceCollection services)
        {
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICityAdminService, CityAdminService>();
            services.AddScoped<ICityImageService, CityImageService>();
        }

        private static void AddHotel(IServiceCollection services)
        {
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IHotelAdminService, HotelAdminService>();
            services.AddScoped<IHotelUserService, HotelUserService>();
            services.AddScoped<IHotelImageService, HotelImageService>();
        }

        private static void AddValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<PaginationDTO>, PaginationValidator>();
            services.AddScoped<IValidator<UserDTO>, UserValidator>();
            services.AddScoped<IValidator<UserLoginDTO>, UserLoginValidator>();
            services.AddScoped<IValidator<RoomDTO>, RoomValidator>();
            services.AddScoped<IValidator<IEnumerable<Image>>, ImagesValidator>();
            services.AddScoped<IValidator<CartItemDTO>, CartItemValidator>();
            services.AddScoped<IValidator<HotelSearchDTO>, HotelSearchValidator>();
            services.AddScoped<IValidator<BookingDTO>, BookingValidator>();
            services.AddScoped<IValidator<DiscountDTO>, DiscountValidator>();
            services.AddScoped<IValidator<HotelReviewDTO>, HotelReviewValidator>();
            services.AddScoped<IValidator<CityDTO>, CityValidator>();
            services.AddScoped<IValidator<HotelDTO>, HotelValidator>();
            services.AddScoped<IValidator<ImageSizeDTO>, ImageSizeValidator>();
        }

        private static void AddAuthentication(
            IServiceCollection services, IConfiguration config)
        {
            var jwtConfig = new JwtConfigurations();
            config
                .GetSection(nameof(JwtConfigurations))
                .Bind(jwtConfig);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    SetTokenValidationParameters(options, jwtConfig);
                });
        }

        private static void SetTokenValidationParameters(
            JwtBearerOptions options, JwtConfigurations jwtConfig)
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidAudience = jwtConfig.Audience,
                ValidIssuer = jwtConfig.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtConfig.Key)),
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
