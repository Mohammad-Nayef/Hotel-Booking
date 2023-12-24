using System.Text.Json.Serialization;

namespace HotelBooking.Api.Models
{
    public class BookingCreationDTO
    {
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public Guid RoomId { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
