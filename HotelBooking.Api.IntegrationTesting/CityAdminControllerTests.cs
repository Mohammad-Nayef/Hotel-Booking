using System.Net;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using HotelBooking.Api.Models.City;
using HotelBooking.Domain.Models.City;
using Microsoft.AspNetCore.JsonPatch;

namespace HotelBooking.Api.IntegrationTesting
{
    public class CityAdminControllerTests : IClassFixture<HotelBookingWebApplicationFactory>
    {
        private readonly HttpClient _guest;
        private readonly HttpClient _user;
        private readonly HttpClient _admin;
        private readonly Fixture _fixture = new();

        public CityAdminControllerTests(HotelBookingWebApplicationFactory factory)
        {
            _guest = factory.GuestClient;
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
            var newCity = new CityCreationDTO();
            var cityId = new Guid();
            var cityPatch = new JsonPatchDocument<CityUpdateDTO>();

            // Act
            var getStatusCode = (await client.GetAsync("api/admin/cities")).StatusCode;
            var searchStatusCode =
                (await client.GetAsync($"api/admin/cities/search?query={searchString}"))
                .StatusCode;
            var postStatusCode = 
                (await client.PostAsJsonAsync("api/admin/cities", newCity))
                .StatusCode;
            var deleteStatusCode = (await client.DeleteAsync($"api/admin/cities/{cityId}"))
                .StatusCode;
            var patchStatusCode = 
                (await client.PatchAsJsonAsync($"api/admin/cities/{cityId}", cityPatch))
                .StatusCode;

            // Assert
            getStatusCode.Should().Be(expectedStatusCode);
            searchStatusCode.Should().Be(expectedStatusCode);
            postStatusCode.Should().Be(expectedStatusCode);
            deleteStatusCode.Should().Be(expectedStatusCode);
            patchStatusCode.Should().Be(expectedStatusCode);
        }

        [Fact]
        public async Task Requests_ShouldBeAuthorizedFor_Admins()
        {
            // Arrange
            var searchString = string.Empty;
            var newCity = new CityCreationDTO();
            var cityId = new Guid();
            var cityPatch = new JsonPatchDocument<CityUpdateDTO>();

            // Act
            var getStatusCode = (await _admin.GetAsync("api/admin/cities")).StatusCode;
            var searchStatusCode =
                (await _admin.GetAsync($"api/admin/cities/search?query={searchString}"))
                .StatusCode;
            var postStatusCode =
                (await _admin.PostAsJsonAsync("api/admin/cities", newCity))
                .StatusCode;
            var deleteStatusCode = (await _admin.DeleteAsync($"api/admin/cities/{cityId}"))
                .StatusCode;
            var patchStatusCode =
                (await _admin.PatchAsJsonAsync($"api/admin/cities/{cityId}", cityPatch))
                .StatusCode;

            // Assert
            getStatusCode.Should().NotBe(HttpStatusCode.Unauthorized);
            searchStatusCode.Should().NotBe(HttpStatusCode.Unauthorized);
            postStatusCode.Should().NotBe(HttpStatusCode.Unauthorized);
            deleteStatusCode.Should().NotBe(HttpStatusCode.Unauthorized);
            patchStatusCode.Should().NotBe(HttpStatusCode.Unauthorized);

            getStatusCode.Should().NotBe(HttpStatusCode.Forbidden);
            searchStatusCode.Should().NotBe(HttpStatusCode.Forbidden);
            postStatusCode.Should().NotBe(HttpStatusCode.Forbidden);
            deleteStatusCode.Should().NotBe(HttpStatusCode.Forbidden);
            patchStatusCode.Should().NotBe(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task CitiesGet_ReturnsOkFor_Admin()
        {
            // Act
            var responseCode = (await _admin.GetAsync("api/admin/cities")).StatusCode;

            // Assert
            responseCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CitiesSearch_ReturnsOkFor_Admin()
        {
            // Arrange
            var searchString = _fixture.Create<string>();

            // Act
            var responseCode = 
                (await _admin.GetAsync($"api/admin/cities/search?query={searchString}"))
                .StatusCode;

            // Assert
            responseCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
