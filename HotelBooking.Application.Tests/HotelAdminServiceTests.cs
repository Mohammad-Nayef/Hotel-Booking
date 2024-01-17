using FluentAssertions;
using FluentValidation;
using HotelBooking.Application.Services.Hotel;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Abstractions.Services.Hotel;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Models;
using Moq;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Tests
{
    public class HotelAdminServiceTests
    {
        private readonly Mock<IValidator<PaginationDTO>> _paginationValidatorMock = new();
        private readonly Mock<IHotelService> _hotelServiceMock = new();
        private readonly Mock<IHotelImageService> _hotelImageServiceMock = new();
        private readonly Mock<IHotelAdminRepository> _hotelAdminRepositoryMock = new();
        private readonly HotelAdminService _hotelAdminService;

        public HotelAdminServiceTests()
        {
            _hotelAdminService = new HotelAdminService(
                _paginationValidatorMock.Object,
                _hotelServiceMock.Object,
                _hotelImageServiceMock.Object,
                _hotelAdminRepositoryMock.Object);
        }

        [Theory]
        [InlineData(ImagesConstants.MaxNumberOfImagesPerEntity, 1)]
        [InlineData(1, ImagesConstants.MaxNumberOfImagesPerEntity)]
        public async Task AddImagesAsync_ThrowsExceptionFor_InvalidNumberOfImages(
            int newImagesCount, int storedImagesCount)
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var images = new List<Image>(new Image[newImagesCount]);
            _hotelImageServiceMock.Setup(x =>
                x.GetCountAsync(hotelId)).ReturnsAsync(storedImagesCount);

            // Act
            var act = async () => await _hotelAdminService.AddImagesAsync(hotelId, images);

            // Assert
            await act.Should().ThrowAsync<EntityImagesLimitExceededException>();
        }

        [Theory]
        [InlineData(ImagesConstants.MaxNumberOfImagesPerEntity - 1, 1)]
        [InlineData(1, ImagesConstants.MaxNumberOfImagesPerEntity - 1)]
        public async Task AddImagesAsync_ShouldNotThrowExceptionFor_ValidNumberOfImages(
            int newImagesCount, int storedImagesCount)
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var images = new List<Image>(new Image[newImagesCount]);
            _hotelImageServiceMock.Setup(x =>
                x.GetCountAsync(hotelId)).ReturnsAsync(storedImagesCount);

            // Act
            var act = async () => await _hotelAdminService.AddImagesAsync(hotelId, images);

            // Assert
            await act.Should().NotThrowAsync<EntityImagesLimitExceededException>();
        }
    }
}
