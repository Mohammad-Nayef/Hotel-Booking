using AutoMapper;
using FluentAssertions;
using HotelBooking.Api.Controllers;
using HotelBooking.Api.Models.User;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace HotelBooking.Api.Tests
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        [Fact]
        public async Task PostUserAsync_ValidUser_ReturnsCreated()
        {
            // Arrange
            _userServiceMock.Setup(x => x.AddAsync(It.IsAny<UserDTO>()))
                .ReturnsAsync(Guid.NewGuid());
            _mapperMock.Setup(x => x.Map<UserCreationResponseDTO>(It.IsAny<UserCreationDTO>()))
                .Returns(new UserCreationResponseDTO());
            var _loggerMock = new Mock<ILogger<AuthenticationController>>();
            var controller = new AuthenticationController(
                _userServiceMock.Object, _mapperMock.Object, _loggerMock.Object);

            // Act
            var result = await controller.PostUserAsync(new UserCreationDTO());

            // Assert
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async Task PostUserAsync_InvalidUser_ReturnsBadRequest()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.AddAsync(It.IsAny<UserDTO>()))
                .ThrowsAsync(new FluentValidation.ValidationException("Invalid user"));
            var mapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<AuthenticationController>>();
            var controller = new AuthenticationController(
                userServiceMock.Object, mapperMock.Object, loggerMock.Object);

            // Act
            var result = await controller.PostUserAsync(new UserCreationDTO());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsOk()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<UserLoginDTO>()))
                .ReturnsAsync("fakeToken");
            var mapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<AuthenticationController>>();
            var controller = new AuthenticationController(
                userServiceMock.Object, mapperMock.Object, loggerMock.Object);

            // Act
            var result = await controller.LoginAsync(new UserLoginDTO());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result;
            okResult.Value.Should().Be("fakeToken");
        }

        [Fact]
        public async Task LoginAsync_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<UserLoginDTO>()))
                .ThrowsAsync(new InvalidUserCredentialsException());
            var mapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<AuthenticationController>>();
            var controller = new AuthenticationController(
                userServiceMock.Object, mapperMock.Object, loggerMock.Object);

            // Act
            var result = await controller.LoginAsync(new UserLoginDTO());

            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }
    }
}
