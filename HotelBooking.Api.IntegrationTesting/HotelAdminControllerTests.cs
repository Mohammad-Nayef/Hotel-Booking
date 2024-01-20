﻿using System.Net;
using AutoFixture;
using FluentAssertions;
using HotelBooking.Api.Models.Hotel;
using Microsoft.AspNetCore.JsonPatch;

namespace HotelBooking.Api.IntegrationTesting
{
    public class HotelAdminControllerTests : IClassFixture<HotelsBookingFactory>
    {
        private readonly HttpClient _guest;
        private readonly HttpClient _user;
        private readonly HttpClient _admin;
        private readonly Fixture _fixture = new();

        public HotelAdminControllerTests(HotelsBookingFactory factory)
        {
            _guest = factory.GetGuestClient();
            _user = factory.GetUserClientAsync().Result;
            _admin = factory.GetAdminClientAsync().Result;
        }

        [Fact]
        public async Task Requests_ReturnsUnauthorizedFor_Guests()
        {
            await ExecuteRequestTests(_guest, HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Requests_ReturnsForbiddenFor_Users()
        {
            await ExecuteRequestTests(_user, HttpStatusCode.Forbidden);
        }

        private async Task ExecuteRequestTests(HttpClient client, HttpStatusCode expectedStatusCode)
        {
            // Arrange
            var searchString = string.Empty;
            var newHotel = new HotelCreationDTO();
            var hotelId = new Guid();
            var hotelPatch = new JsonPatchDocument<HotelUpdateDTO>();
            var statusCodes = new List<HttpStatusCode>();

            // Act
            var getStatusCode = (await client.GetAsync("api/admin/hotels")).StatusCode;
            var searchStatusCode =
                (await client.GetAsync($"api/admin/hotels/search?query={searchString}"))
                .StatusCode;
            var postStatusCode =
                (await client.PostAsJsonAsync("api/admin/hotels", newHotel))
                .StatusCode;
            var deleteStatusCode = (await client.DeleteAsync($"api/admin/hotels/{hotelId}"))
                .StatusCode;
            var patchStatusCode =
                (await client.PatchAsJsonAsync($"api/admin/hotels/{hotelId}", hotelPatch))
                .StatusCode;
            statusCodes.AddRange(
                [getStatusCode, searchStatusCode, postStatusCode, deleteStatusCode, patchStatusCode]);

            // Assert
            statusCodes.ForEach(statusCode => statusCode.Should().Be(expectedStatusCode));
        }

        [Fact]
        public async Task Requests_ShouldBeAuthorizedFor_Admins()
        {
            // Arrange
            var searchString = string.Empty;
            var newHotel = new HotelCreationDTO();
            var hotelId = new Guid();
            var hotelPatch = new JsonPatchDocument<HotelUpdateDTO>();
            var statusCodes = new List<HttpStatusCode>();

            // Act
            var getStatusCode = (await _admin.GetAsync("api/admin/hotels")).StatusCode;
            var searchStatusCode =
                (await _admin.GetAsync($"api/admin/hotels/search?query={searchString}"))
                .StatusCode;
            var postStatusCode =
                (await _admin.PostAsJsonAsync("api/admin/hotels", newHotel))
                .StatusCode;
            var deleteStatusCode = (await _admin.DeleteAsync($"api/admin/hotels/{hotelId}"))
                .StatusCode;
            var patchStatusCode =
                (await _admin.PatchAsJsonAsync($"api/admin/hotels/{hotelId}", hotelPatch))
                .StatusCode;
            statusCodes.AddRange(
                [getStatusCode, searchStatusCode, postStatusCode, deleteStatusCode, patchStatusCode]);

            // Assert
            statusCodes.ForEach(statusCode =>
                statusCode.Should().NotBe(HttpStatusCode.Unauthorized)
                .And.NotBe(HttpStatusCode.Forbidden));
        }

        [Fact]
        public async Task GetHotels_ReturnsOkFor_Admin()
        {
            // Act
            var responseCode = (await _admin.GetAsync("api/admin/hotels")).StatusCode;

            // Assert
            responseCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task SearchHotels_ReturnsOkFor_Admin()
        {
            // Arrange
            var searchString = _fixture.Create<string>();

            // Act
            var responseCode =
                (await _admin.GetAsync($"api/admin/hotels/search?query={searchString}"))
                .StatusCode;

            // Assert
            responseCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
