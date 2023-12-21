using FluentValidation;
using HotelBooking.Application.Services;
using HotelBooking.Application.Validators;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Entities;
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
            services.AddScoped<IValidator<HotelDTO>, HotelValidator>();
            services.AddScoped<IValidator<RoomDTO>, RoomValidator>();
            services.AddScoped<IValidator<CityDTO>, CityValidator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IRoomService, RoomService>();

            return services;
        }
    }
}
