using AutoFixture;
using FluentAssertions;
using FluentValidation;
using HotelBooking.Application.Services;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Abstractions.Repositories.Room;
using HotelBooking.Domain.Abstractions.Utilities;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Domain.Models.Room;
using HotelBooking.Domain.Models.User;
using Moq;

namespace HotelBooking.Application.Tests
{
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> _bookingRepositoryMock = new();
        private readonly Mock<IValidator<BookingDTO>> _bookingValidatorMock = new();
        private readonly Mock<IHotelDiscountRepository> _hotelDiscountRepositoryMock = new();
        private readonly Mock<IRoomRepository> _roomRepositoryMock = new();
        private readonly Mock<IEmailService> _emailServiceMock = new();
        private readonly Mock<IHotelRepository> _hotelRepositoryMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly BookingService _bookingService;
        private readonly Fixture _fixture = new();

        public BookingServiceTests()
        {
            _bookingService = new(
                _bookingRepositoryMock.Object,
                _bookingValidatorMock.Object,
                _hotelDiscountRepositoryMock.Object,
                _roomRepositoryMock.Object,
                _emailServiceMock.Object,
                _userRepositoryMock.Object,
                _hotelRepositoryMock.Object);
        }

        [Theory]
        [InlineData(100, 1, 0, 100)]
        [InlineData(100, 1, 10, 90)]
        [InlineData(100, 2, 10, 180)]
        public async Task AddAsync_AddsValidBookingPrice(
            decimal roomPrice, int days, float discountPercentage, decimal finalPrice)
        {
            // Arrange
            var booking = GetBooking(days);
            var discount = _fixture.Build<DiscountDTO>()
                .With(x => x.AmountPercent, discountPercentage)
                .Create();
            _hotelDiscountRepositoryMock.Setup(x =>
                x.GetHighestActiveDiscount(It.IsAny<Guid>())).Returns(discount);
            SetupMocks(roomPrice);

            // Act
            await _bookingService.AddAsync(booking);

            // Assert
            booking.Price.Should().Be(finalPrice);
        }

        [Theory]
        [InlineData(100, 1, 100)]
        public async Task AddAsync_HandlesFinalPriceFor_NullDiscount(
            decimal roomPrice, int days, decimal finalPrice)
        {
            // Arrange
            var booking = GetBooking(days);
            _hotelDiscountRepositoryMock.Setup(x =>
                x.GetHighestActiveDiscount(It.IsAny<Guid>())).Returns((DiscountDTO)null);
            SetupMocks(roomPrice);

            // Act
            await _bookingService.AddAsync(booking);

            // Assert
            booking.Price.Should().Be(finalPrice);
        }

        private void SetupMocks(decimal roomPrice)
        {
            var room = _fixture.Build<RoomDTO>()
                .With(x => x.PricePerNight, roomPrice)
                .Create();
            _roomRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(room);
            _hotelRepositoryMock.Setup(x =>
                x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_fixture.Create<HotelDTO>());
            _userRepositoryMock.Setup(x =>
                x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_fixture.Create<UserDTO>);
        }

        private BookingDTO GetBooking(int days)
        {
            var booking = _fixture.Build<BookingDTO>()
                .With(x => x.StartingDate, DateTime.Now)
                .With(x => x.EndingDate, DateTime.Now.AddDays(days - 1))
                .Create();

            return booking;
        }
    }
}
