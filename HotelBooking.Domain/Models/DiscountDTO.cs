using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    /// <inheritdoc cref="Entities.Discount"/>
    public class DiscountDTO : Entity
    {
        /// <inheritdoc cref="Entities.Discount.Reason"/>
        public string? Reason { get; set; }

        /// <inheritdoc cref="Entities.Discount.StartingDate"/>
        public DateTime StartingDate { get; set; }

        /// <inheritdoc cref="Entities.Discount.EndingDate"/>
        public DateTime EndingDate { get; set; }

        /// <inheritdoc cref="Entities.Discount.AmountPercent"/>
        public float AmountPercent { get; set; }

        /// <summary>
        /// Id of the hotel that has the discount.
        /// </summary>
        public Guid HotelId { get; set; }
    }
}
