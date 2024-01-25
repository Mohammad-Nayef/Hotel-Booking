using System.Net;
using FluentAssertions;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Models.User;

namespace HotelBooking.Api.IntegrationTesting
{
    public class AuthenticationControllerTests : IClassFixture<HotelsBookingFactory>
    {
        private readonly HttpClient _guest;

        public AuthenticationControllerTests(HotelsBookingFactory factory)
        {
            _guest = factory.GetGuestClient();
        }

        [Fact]
        public async Task GuestClient_CanAccessRegister_ForInvalidRequestBody()
        {
            // Arrange
            var user = new UserCreationDTO();

            // Act
            var response = await _guest.PostAsJsonAsync("api/auth/user-register", user);

            // Assert
            response.StatusCode.Should().NotBe(HttpStatusCode.Unauthorized)
                .And.NotBe(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task GuestClient_CanAccessLogin_ForInvalidRequestBody()
        {
            // Arrange
            var user = new UserLoginDTO();

            // Act
            var response = await _guest.PostAsJsonAsync("api/auth/user-login", user);

            // Assert
            response.StatusCode.Should().NotBe(HttpStatusCode.Unauthorized)
                .And.NotBe(HttpStatusCode.Forbidden);
        }
    }
}
