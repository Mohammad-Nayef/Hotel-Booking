using System.Text.Json.Serialization;

namespace HotelBooking.Api.Models
{
    /// <summary>
    /// Model to create new discount.
    /// </summary>
    public class DiscountCreationDTO
    {
        /// <summary>
        /// Reason of the discount.
        /// Must be of length between 0 and 200.
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
        /// Must be between 0 and 100.
        /// </summary>
        public float AmountPercent { get; set; }

        /// <summary>
        /// Id of the hotel that has the discount.
        /// </summary>
        [JsonIgnore]
        public Guid HotelId { get; set; }
    }
}
