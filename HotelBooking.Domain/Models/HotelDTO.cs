using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class HotelDTO : Entity
    {
        public string Name { get; set; }
        public string BriefDescription { get; set; }
        public string FullDescription { get; set; }
        public float StarRating { get; set; }
        public string OwnerName { get; set; }
        public string Geolocation { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Guid CityId { get; set; }
    }
}
