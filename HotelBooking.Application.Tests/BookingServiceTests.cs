using FluentValidation;
using HotelBooking.Application.Services;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Abstractions.Repositories.Room;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;
using Moq;
using HotelBooking.Domain.Models.Room;
using FluentAssertions;

namespace HotelBooking.Application.Tests
{
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> _bookingRepositoryMock = new();
        private readonly Mock<IValidator<BookingDTO>> _bookingValidatorMock = new();
        private readonly Mock<IHotelDiscountRepository> _hotelDiscountRepositoryMock = new();
        private readonly Mock<IRoomRepository> _roomRepositoryMock = new();
        private readonly BookingService _bookingService;

        public BookingServiceTests()
        {
            _bookingService = new(
                _bookingRepositoryMock.Object, 
                _bookingValidatorMock.Object, 
                _hotelDiscountRepositoryMock.Object, 
                _roomRepositoryMock.Object);
        }

        [Theory]
        [InlineData(100, 1, 0, 100)]
        [InlineData(100, 1, 10, 90)]
        [InlineData(100, 2, 10, 180)]
        public async Task AddAsync_AddsValidBookingPrice(
            decimal roomPrice, int days, float discountPercentage, decimal finalPrice)
        {
            // Arrange
            var booking = GetBooking(roomPrice, days);
            var discount = new DiscountDTO { AmountPercent = discountPercentage };
            _hotelDiscountRepositoryMock.Setup(x =>
                x.GetHighestActiveDiscountAsync(It.IsAny<Guid>())).ReturnsAsync(discount);

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
            var booking = GetBooking(roomPrice, days);
            _hotelDiscountRepositoryMock.Setup(x =>
                x.GetHighestActiveDiscountAsync(It.IsAny<Guid>())).ReturnsAsync((DiscountDTO)null);

            // Act
            await _bookingService.AddAsync(booking);

            // Assert
            booking.Price.Should().Be(finalPrice);
        }

        private BookingDTO GetBooking(decimal roomPrice, int days)
        {
            var room = new RoomDTO { PricePerNight = roomPrice };
            _roomRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(room);
            var booking = new BookingDTO
            {
                StartingDate = DateTime.Now,
                EndingDate = DateTime.Now.AddDays(days - 1)
            };
            return booking;
        }
    }
}
