using System.Text;
using FluentValidation;
using HotelBooking.Application.Services;
using HotelBooking.Application.Validators;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;
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
        public static IServiceCollection AddDomainServices(
            this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IValidator<PaginationDTO>, PaginationValidator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IValidator<UserDTO>, UserValidator>();
            services.AddScoped<IValidator<UserLoginDTO>, UserLoginValidator>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IValidator<HotelDTO>, HotelValidator>();
            services.AddScoped<IHotelReviewService, HotelReviewService>();
            services.AddScoped<IValidator<HotelReviewDTO>, HotelReviewValidator>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IValidator<CityDTO>, CityValidator>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IValidator<RoomDTO>, RoomValidator>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IValidator<DiscountDTO>, DiscountValidator>();
            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<IValidator<CartItemDTO>, CartItemValidator>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IValidator<BookingDTO>, BookingValidator>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IValidator<IEnumerable<Image>>, ImagesValidator>();
            services.AddScoped<IAuthTokenProcessor, AuthTokenProcessor>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidAudience = config["Jwt:Audience"],
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
