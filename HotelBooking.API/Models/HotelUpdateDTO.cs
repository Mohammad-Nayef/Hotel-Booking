namespace HotelBooking.Api.Models
{
    public class HotelUpdateDTO
    {
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string Geolocation { get; set; }
        public Guid CityId { get; set; }
    }
}
