namespace HotelBooking.Api.Models
{
    public class HotelCreationDTO
    {
        public string Name { get; set; }
        public string BriefDescription { get; set; }
        public string FullDescription { get; set; }
        public float StarRating { get; set; }
        public string OwnerName { get; set; }
        public string Geolocation { get; set; }
        public Guid CityId { get; set; }
    }
}
