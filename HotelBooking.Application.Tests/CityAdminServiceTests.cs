using FluentAssertions;
using FluentValidation;
using HotelBooking.Application.Services.City;
using HotelBooking.Domain.Abstractions.Repositories.City;
using HotelBooking.Domain.Abstractions.Services.City;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Models;
using Moq;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Tests
{
    public class CityAdminServiceTests
    {
        private readonly Mock<IValidator<PaginationDTO>> _paginationValidatorMock = new();
        private readonly Mock<ICityService> _cityServiceMock = new();
        private readonly Mock<ICityImageService> _cityImageServiceMock = new();
        private readonly Mock<ICityAdminRepository> _cityAdminRepositoryMock = new();
        private readonly CityAdminService _cityAdminService;

        public CityAdminServiceTests()
        {
            _cityAdminService = new CityAdminService(
                _paginationValidatorMock.Object,
                _cityServiceMock.Object,
                _cityImageServiceMock.Object,
                _cityAdminRepositoryMock.Object);
        }

        [Theory]
        [InlineData(ImagesConstants.MaxNumberOfImagesPerEntity, 1)]
        [InlineData(1, ImagesConstants.MaxNumberOfImagesPerEntity)]
        public async Task AddImagesAsync_ThrowsExceptionFor_InvalidNumberOfImages(
            int newImagesCount, int storedImagesCount)
        {
            // Arrange
            var cityId = Guid.NewGuid();
            var images = new List<Image>(new Image[newImagesCount]);
            _cityImageServiceMock.Setup(x =>
                x.GetCountAsync(cityId)).ReturnsAsync(storedImagesCount);

            // Act
            var act = async () => await _cityAdminService.AddImagesAsync(cityId, images);

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
            var cityId = Guid.NewGuid();
            var images = new List<Image>(new Image[newImagesCount]);
            _cityImageServiceMock.Setup(x =>
                x.GetCountAsync(cityId)).ReturnsAsync(storedImagesCount);

            // Act
            var act = async () => await _cityAdminService.AddImagesAsync(cityId, images);

            // Assert
            await act.Should().NotThrowAsync<EntityImagesLimitExceededException>();
        }
    }
}
