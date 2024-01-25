using System.Net;
using FluentAssertions;
using HotelBooking.Api.Models;

namespace HotelBooking.Api.IntegrationTesting
{
    public class UserControllerTests : IClassFixture<HotelsBookingFactory>
    {
        private readonly HttpClient _guest;
        private readonly HttpClient _user;

        public UserControllerTests(HotelsBookingFactory factory)
        {
            _guest = factory.GetGuestClient();
            _user = factory.GetUserClientAsync().Result;
        }

        [Fact]
        public async Task Requests_ReturnUnauthorizedFor_Guest()
        {
            // Arrange
            var cartItem = new CartItemCreationDTO();
            var booking = new BookingCreationDTO();
            var review = new HotelReviewCreationDTO();
            var statusCodes = new List<HttpStatusCode>();

            // Act
            var postCartItemStatusCode =
                (await _guest.PostAsJsonAsync($"api/users/current-user/cart-items", cartItem))
                .StatusCode;
            var getCartItemsStatusCode =
                (await _guest.GetAsync($"api/users/current-user/cart-items"))
                .StatusCode;
            var postBookingStatusCode =
                (await _guest.PostAsJsonAsync($"api/users/current-user/bookings", booking))
                .StatusCode;
            var postReviewStatusCode =
                (await _guest.PostAsJsonAsync($"api/users/current-user/hotel-reviews", review))
                .StatusCode;
            statusCodes.AddRange(
                [
                    postCartItemStatusCode,
                    getCartItemsStatusCode,
                    postBookingStatusCode,
                    postReviewStatusCode
                ]);

            // Assert
            statusCodes.ForEach(statusCode => statusCode.Should().Be(HttpStatusCode.Unauthorized));
        }

        [Fact]
        public async Task GetCartItems_ReturnsOkFor_User()
        {
            // Act
            var getFeaturedStatusCode =
                (await _user.GetAsync("api/users/current-user/cart-items"))
                .StatusCode;

            // Assert
            getFeaturedStatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
