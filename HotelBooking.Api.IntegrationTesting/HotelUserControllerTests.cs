﻿using System.Net;
using AutoFixture;
using FluentAssertions;
using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Api.IntegrationTesting
{
    public class HotelUserControllerTests : IClassFixture<HotelBookingWebApplicationFactory>
    {
        private readonly HttpClient _guest;
        private readonly HttpClient _user;
        private readonly Fixture _fixture = new();

        public HotelUserControllerTests(HotelBookingWebApplicationFactory factory)
        {
            _guest = factory.GuestClient;
            _user = factory.GetUserClientAsync().Result;
        }

        [Fact]
        public async Task Requests_ReturnUnauthorizedFor_Guest()
        {
            // Arrange
            var hotelId = new Guid();
            var search = new HotelSearchDTO();
            var statusCodes = new List<HttpStatusCode>();

            // Act
            var getHotelStatusCode = (await _guest.GetAsync($"api/hotels/{hotelId}")).StatusCode;
            var getReviewsStatusCode = 
                (await _guest.GetAsync($"api/hotels/{hotelId}/reviews"))
                .StatusCode;
            var getFeaturedStatusCode =
                (await _guest.GetAsync($"api/hotels/featured"))
                .StatusCode;
            var searchStatusCode = 
                (await _guest.PostAsJsonAsync("api/hotels/search", search))
                .StatusCode;
            var roomsStatusCode =
                (await _guest.GetAsync($"api/hotels/{hotelId}/rooms/available"))
                .StatusCode;
            statusCodes.AddRange(
                [
                    getHotelStatusCode, 
                    getReviewsStatusCode, 
                    getFeaturedStatusCode, 
                    searchStatusCode, 
                    roomsStatusCode
                ]);

            // Assert
            statusCodes.ForEach(statusCode => statusCode.Should().Be(HttpStatusCode.Unauthorized));
        }

        [Fact]
        public async Task GetFeaturedHotels_ReturnsOkFor_User()
        {
            // Act
            var getFeaturedStatusCode = (await _user.GetAsync("api/hotels/featured")).StatusCode;

            // Assert
            getFeaturedStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task SearchHotels_ReturnsOkFor_User()
        {
            // Arrange
            var search = new HotelSearchDTO { SearchQuery = _fixture.Create<string>() };

            // Act
            var searchStatusCode = 
                (await _user.PostAsJsonAsync("api/hotels/search", search))
                .StatusCode;

            // Assert
            searchStatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}