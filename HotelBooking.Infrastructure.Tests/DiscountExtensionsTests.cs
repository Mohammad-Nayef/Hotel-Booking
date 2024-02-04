using FluentAssertions;
using HotelBooking.Infrastructure.Extensions;
using HotelBooking.Infrastructure.Tables;

namespace HotelBooking.Infrastructure.Tests
{
    public class DiscountExtensionsTests
    {
        [Theory]
        [InlineData(-2, -1)]
        [InlineData(1, 2)]
        public void GetHighestActive_ReturnsNullFor_NotActiveDiscounts(
            int daysToStart, int daysToEnd)
        {
            // Arrange
            var discounts = new List<DiscountTable>
            {
                new DiscountTable
                {
                    StartingDate = DateTime.Today.AddDays(daysToStart),
                    EndingDate = DateTime.Today.AddDays(daysToEnd)
                }
            };

            // Act
            var highestActiveDiscount = discounts.GetHighestActive();

            // Assert
            highestActiveDiscount.Should().BeNull();
        }

        [Theory]
        [InlineData(1, 1, 1, 1)]
        [InlineData(1, 2, 3, 3)]
        public void GetHighestActive_ReturnsHighestDiscountFor_ActiveDiscounts(
            float discountPercentage1,
            float discountPercentage2,
            float discountPercentage3,
            float result)
        {
            // Arrange
            var discounts = GetDiscounts(
                discountPercentage1, discountPercentage2, discountPercentage3);

            // Act
            var highestActiveDiscountPercentage = discounts.GetHighestActive().AmountPercent;

            // Assert
            highestActiveDiscountPercentage.Should().Be(result);
        }

        private static List<DiscountTable> GetDiscounts(
            float discountPercentage1, float discountPercentage2, float discountPercentage3)
        {
            var startingDate = DateTime.Today.AddDays(-1);
            var endingDate = DateTime.Today.AddDays(1);
            var discounts = new List<DiscountTable>
            {
                new DiscountTable
                {
                    StartingDate = startingDate,
                    EndingDate = endingDate,
                    AmountPercent =discountPercentage1
                },
                new DiscountTable
                {
                    StartingDate = startingDate,
                    EndingDate = endingDate,
                    AmountPercent = discountPercentage2
                },
                new DiscountTable
                {
                    StartingDate = startingDate,
                    EndingDate = endingDate,
                    AmountPercent = discountPercentage3
                }
            };

            return discounts;
        }

        [Fact]
        public void HasActiveDiscount_ReturnsTrueFor_ActiveDiscount()
        {
            // Arrange
            var discounts = new List<DiscountTable>
            {
                new DiscountTable
                {
                    StartingDate = DateTime.Today.AddDays(-1),
                    EndingDate = DateTime.Today.AddDays(1)
                }
            };

            // Act
            var hasActiveDiscount = discounts.HasActiveDiscount();

            // Assert
            hasActiveDiscount.Should().BeTrue();
        }

        [Theory]
        [InlineData(-2, -1)]
        [InlineData(1, 2)]
        public void HasActiveDiscount_ReturnsFalseFor_InactiveDiscounts(
            int daysToStart, int daysToEnd)
        {
            // Arrange
            var discounts = new List<DiscountTable>
            {
                new DiscountTable
                {
                    StartingDate = DateTime.Today.AddDays(daysToStart),
                    EndingDate = DateTime.Today.AddDays(daysToEnd)
                }
            };

            // Act
            var hasActiveDiscount = discounts.HasActiveDiscount();

            // Assert
            hasActiveDiscount.Should().BeFalse();
        }

        [Theory]
        [InlineData(-1, 1)]
        [InlineData(0, 1)]
        public void IsActive_ReturnsTrueFor_ActiveDiscount(int daysToStart, int daysToEnd)
        {
            // Arrange
            var activeDiscount = new DiscountTable
            {
                StartingDate = DateTime.Today.AddDays(daysToStart),
                EndingDate = DateTime.Today.AddDays(daysToEnd)
            };

            // Act
            var isActive = activeDiscount.IsActive();

            // Assert
            isActive.Should().BeTrue();
        }

        [Theory]
        [InlineData(-2, -1)]
        [InlineData(1, 2)]
        public void IsActive_ReturnsTrueFor_InactiveDiscount(int daysToStart, int daysToEnd)
        {
            // Arrange
            var activeDiscount = new DiscountTable
            {
                StartingDate = DateTime.Today.AddDays(daysToStart),
                EndingDate = DateTime.Today.AddDays(daysToEnd)
            };

            // Act
            var isActive = activeDiscount.IsActive();

            // Assert
            isActive.Should().BeFalse();
        }
    }
}
