using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Entities
{
    /// <summary>
    /// Time-limited discount for an hotel.
    /// </summary>
    public class Discount : Entity
    {
        /// <summary>
        /// Reason of the discount.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="DiscountConstants.MinReasonLength"/> and 
        /// <see cref="DiscountConstants.MaxReasonLength"/>.
        /// </remarks>
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
        /// <remarks>        
        /// Must be between <see cref="DiscountConstants.MinAmountPercent"/> and 
        /// <see cref="DiscountConstants.MaxAmountPercent"/>.
        /// </remarks>        
        public float AmountPercent { get; set; }

        public Hotel Hotel { get; set; }
    }
}
