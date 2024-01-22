using HotelBooking.Domain.Models;

namespace HotelBooking.Infrastructure.Tables
{
    /// <inheritdoc cref="DiscountDTO"/>
    internal class DiscountTable : DbEntity
    {
        /// <inheritdoc cref="DiscountDTO.Reason"/>
        public string? Reason { get; set; }

        /// <inheritdoc cref="DiscountDTO.StartingDate"/>
        public DateTime StartingDate { get; set; }

        /// <inheritdoc cref="DiscountDTO.EndingDate"/>
        public DateTime EndingDate { get; set; }

        /// <inheritdoc cref="DiscountDTO.AmountPercent"/>
        public float AmountPercent { get; set; }

        /// <inheritdoc cref="DiscountDTO.HotelId"/>
        public Guid HotelId { get; set; }

        public HotelTable Hotel { get; set; }
    }
}
