using System.Net;
using FluentAssertions;

namespace HotelBooking.Api.IntegrationTesting
{
    public class CityUserControllerTest : IClassFixture<HotelsBookingFactory>
    {
        private readonly HttpClient _guest;
        private readonly HttpClient _user;

        public CityUserControllerTest(HotelsBookingFactory factory)
        {
            _guest = factory.GetGuestClient();
            _user = factory.GetUserClientAsync().Result;
        }

        [Fact]
        public async Task Requests_ReturnsUnauthorizedFor_Guest()
        {
            // Act
            var getPopularCitiesStatusCode =
                (await _guest.GetAsync("api/cities/popular"))
                .StatusCode;

            // Assert
            getPopularCitiesStatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetPopularCities_ReturnsOkFor_User()
        {
            // Act
            var getPopularCitiesStatusCode =
                (await _user.GetAsync("api/cities/popular"))
                .StatusCode;

            // Assert
            getPopularCitiesStatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
