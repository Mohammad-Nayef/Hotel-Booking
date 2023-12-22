namespace HotelBooking.Api.Models
{
    public class RoomCreationResponseDTO
    {
        public Guid Id { get; set; }
        public double RoomNumber { get; set; }
        public string Type { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public string BriefDescription { get; set; }
        public decimal PricePerNight { get; set; }
        public Guid HotelId { get; set; }
    }
}
