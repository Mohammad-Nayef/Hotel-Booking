using HotelBooking.Api.Models.User;
using HotelBooking.Domain.Models.User;
using HotelBooking.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HotelBooking.Api.IntegrationTesting
{
    public class HotelBookingWebApplicationFactory : 
        IClassFixture<HotelBookingWebApplicationFactory>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public HotelBookingWebApplicationFactory()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        services.AddDbContext<HotelsBookingDbContext>();
                    }));
        }

        public HttpClient GuestClient => _factory.CreateClient();

        public async Task<HttpClient> GetUserClientAsync()
        {
            var user = _factory.CreateClient();
            var newUser = new UserCreationDTO
            {
                FirstName = "Regular",
                LastName = "User",
                Email = "test@test",
                Username = "test",
                Password = "test1234"
            };
            await user.PostAsJsonAsync("api/auth/user-register", newUser);
            var userLogin = new UserLoginDTO
            {
                Username = "test",
                Password = "test1234"
            };
            var response = await user.PostAsJsonAsync("api/auth/user-login", userLogin);
            user.DefaultRequestHeaders.Authorization =
                new("Bearer", await response.Content.ReadAsStringAsync());

            return user;
        }

        public async Task<HttpClient> GetAdminClientAsync()
        {
            var admin = _factory.CreateClient();
            var adminLogin = new UserLoginDTO
            {
                Username = "admin",
                Password = "admin123"
            };
            var response = await admin.PostAsJsonAsync("api/auth/user-login", adminLogin);
            admin.DefaultRequestHeaders.Authorization =
                new("Bearer", await response.Content.ReadAsStringAsync());

            return admin;
        }
    }
}