using FluentAssertions;
using HotelBooking.Infrastructure.Extensions;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Tests
{
    public class BookingExtensionsTests
    {
        [Theory]
        [InlineData("2000-01-10", "2000-01-12", "2000-01-09", "2000-01-11")]
        [InlineData("2000-01-10", "2000-01-12", "2000-01-11", "2000-01-13")]
        [InlineData("2000-01-10", "2000-01-12", "2000-01-10", "2000-01-12")]
        [InlineData("2000-01-10", "2000-01-13", "2000-01-11", "2000-01-12")]
        public void IntersectsWith_ReturnsTrueFor_IntersectingInterval(
            string bookingStartingDateString,
            string bookingEndingDateString,
            string givenStartingDateString,
            string givenEndingDateString)
        {
            // Arrange
            var bookingStartingDate = DateTime.Parse(bookingStartingDateString);
            var bookingEndingDate = DateTime.Parse(bookingEndingDateString);
            var givenStartingDate = DateTime.Parse(givenStartingDateString);
            var givenEndingDate = DateTime.Parse(givenEndingDateString);
            var booking = new BookingTable
            {
                StartingDate = bookingStartingDate,
                EndingDate = bookingEndingDate
            };

            // Act
            var intersects = booking.IntersectsWith(givenStartingDate, givenEndingDate);

            // Assert
            intersects.Should().BeTrue();
        }

        [Theory]
        [InlineData("2000-01-10", "2000-01-12", "2000-01-08", "2000-01-9")]
        [InlineData("2000-01-10", "2000-01-12", "2000-01-13", "2000-01-14")]
        public void IntersectsWith_ReturnsFalseFor_NotIntersectingInterval(
            string bookingStartingDateString,
            string bookingEndingDateString,
            string givenStartingDateString,
            string givenEndingDateString)
        {
            // Arrange
            var bookingStartingDate = DateTime.Parse(bookingStartingDateString);
            var bookingEndingDate = DateTime.Parse(bookingEndingDateString);
            var givenStartingDate = DateTime.Parse(givenStartingDateString);
            var givenEndingDate = DateTime.Parse(givenEndingDateString);
            var booking = new BookingTable
            {
                StartingDate = bookingStartingDate,
                EndingDate = bookingEndingDate
            };

            // Act
            var intersects = booking.IntersectsWith(givenStartingDate, givenEndingDate);

            // Assert
            intersects.Should().BeFalse();
        }

        [Theory]
        [InlineData("2000-01-10", "2000-01-12", "2000-01-10")]
        [InlineData("2000-01-10", "2000-01-12", "2000-01-11")]
        [InlineData("2000-01-10", "2000-01-12", "2000-01-12")]
        public void IntersectsWith_ReturnsTrueFor_IntersectingDate(
            string bookingStartingDateString,
            string bookingEndingDateString,
            string givenDateString)
        {
            // Arrange
            var bookingStartingDate = DateTime.Parse(bookingStartingDateString);
            var bookingEndingDate = DateTime.Parse(bookingEndingDateString);
            var givenDate = DateTime.Parse(givenDateString);
            var booking = new BookingTable
            {
                StartingDate = bookingStartingDate,
                EndingDate = bookingEndingDate
            };

            // Act
            var intersects = booking.IntersectsWith(givenDate);

            // Assert
            intersects.Should().BeTrue();
        }

        [Theory]
        [InlineData("2000-01-10", "2000-01-12", "2000-01-9")]
        [InlineData("2000-01-10", "2000-01-12", "2000-01-13")]
        public void IntersectsWith_ReturnsFalseFor_NotIntersectingDate(
            string bookingStartingDateString,
            string bookingEndingDateString,
            string givenDateString)
        {
            // Arrange
            var bookingStartingDate = DateTime.Parse(bookingStartingDateString);
            var bookingEndingDate = DateTime.Parse(bookingEndingDateString);
            var givenDate = DateTime.Parse(givenDateString);
            var booking = new BookingTable
            {
                StartingDate = bookingStartingDate,
                EndingDate = bookingEndingDate
            };

            // Act
            var intersects = booking.IntersectsWith(givenDate);

            // Assert
            intersects.Should().BeFalse();
        }
    }
}