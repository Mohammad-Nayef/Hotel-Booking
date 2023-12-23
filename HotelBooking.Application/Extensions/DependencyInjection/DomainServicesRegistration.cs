using FluentValidation;
using HotelBooking.Application.Services;
using HotelBooking.Application.Validators;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.Application.Extensions.DependencyInjection
{
    public static class DomainServicesRegistration
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<PaginationDTO>, PaginationValidator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IValidator<UserDTO>, UserValidator>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IValidator<HotelDTO>, HotelValidator>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IValidator<CityDTO>, CityValidator>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IValidator<RoomDTO>, RoomValidator>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IValidator<DiscountDTO>, DiscountValidator>();

            return services;
        }
    }
}
