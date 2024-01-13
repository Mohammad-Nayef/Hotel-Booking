using System.Text.Json.Serialization;

namespace HotelBooking.Api.Models.Hotel
{
    public class HotelReviewCreationDTO
    {
        public string Content { get; set; }
        public Guid HotelId { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
