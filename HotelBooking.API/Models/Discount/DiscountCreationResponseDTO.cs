using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Api.Models.Discount
{
    /// <summary>
    /// Response for creating new discount.
    /// </summary>
    public class DiscountCreationResponseDTO : Entity
    {
        /// <summary>
        /// Reason of the discount.
        /// </summary>
        public string? Reason { get; set; }

        /// <summary>
        /// Starting time for the discount to be applied.
        /// </summary>
        public DateTime StartingDate { get; set; }

        /// <summary>
        /// Ending time for the discount to be applied.
        /// </summary>
        public DateTime EndingDate { get; set; }

        /// <summary>
        /// Amount of the discount.
        /// </summary>
        public float AmountPercent { get; set; }

        /// <summary>
        /// Id of the hotel that has the discount.
        /// </summary>
        public Guid HotelId { get; set; }
    }
}
