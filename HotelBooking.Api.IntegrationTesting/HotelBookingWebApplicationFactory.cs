using HotelBooking.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HotelBooking.Api.IntegrationTesting
{
    internal class HotelBookingWebApplicationFactory : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        internal HotelBookingWebApplicationFactory(WebApplicationFactory<Program> factory)
        {
            factory = factory.WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(HotelsBookingDbContext));

                    services.AddDbContext<HotelsBookingDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });
                }));
            _factory = factory;
        }
    }
}