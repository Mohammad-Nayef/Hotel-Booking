using System.Text.Json.Serialization;

namespace HotelBooking.Api.Models
{
    public class DiscountCreationDTO
    {
        public string? Reason { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public float AmountPercent { get; set; }
        [JsonIgnore]
        public Guid HotelId { get; set; }
    }
}
