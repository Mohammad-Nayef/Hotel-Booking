using FluentAssertions;
using FluentValidation;
using HotelBooking.Application.Services.Room;
using HotelBooking.Domain.Abstractions.Repositories.Room;
using HotelBooking.Domain.Abstractions.Services.Room;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Models;
using Moq;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Tests
{
    public class RoomAdminServiceTests
    {
        private readonly Mock<IValidator<PaginationDTO>> _paginationValidatorMock = new();
        private readonly Mock<IRoomService> _roomServiceMock = new();
        private readonly Mock<IRoomImageService> _roomImageServiceMock = new();
        private readonly Mock<IRoomAdminRepository> _roomAdminRepositoryMock = new();
        private readonly RoomAdminService _roomAdminService;

        public RoomAdminServiceTests()
        {
            _roomAdminService = new RoomAdminService(
                _paginationValidatorMock.Object,
                _roomServiceMock.Object,
                _roomImageServiceMock.Object,
                _roomAdminRepositoryMock.Object);
        }

        [Theory]
        [InlineData(ImagesConstants.MaxNumberOfImagesPerEntity, 1)]
        [InlineData(1, ImagesConstants.MaxNumberOfImagesPerEntity)]
        public async Task AddImagesAsync_ThrowsExceptionFor_InvalidNumberOfImages(
            int newImagesCount, int storedImagesCount)
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var images = new List<Image>(new Image[newImagesCount]);
            _roomImageServiceMock.Setup(x =>
                x.GetCountAsync(roomId)).ReturnsAsync(storedImagesCount);

            // Act
            var act = async () => await _roomAdminService.AddImagesAsync(roomId, images);

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
            var roomId = Guid.NewGuid();
            var images = new List<Image>(new Image[newImagesCount]);
            _roomImageServiceMock.Setup(x =>
                x.GetCountAsync(roomId)).ReturnsAsync(storedImagesCount);

            // Act
            var act = async () => await _roomAdminService.AddImagesAsync(roomId, images);

            // Assert
            await act.Should().NotThrowAsync<EntityImagesLimitExceededException>();
        }
    }
}
