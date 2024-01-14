using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Entities
{
    public class Hotel : Entity
    {
        public string Name { get; set; }
        public string BriefDescription { get; set; }
        public string FullDescription { get; set; }
        public float StarRating { get; set; }
        public string OwnerName { get; set; }
        public string Geolocation { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public List<HotelReview> Reviews { get; set; }
        public City City { get; set; }
        public List<Image> Images { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Discount> Discounts { get; set; }
        public List<HotelVisit> Visits { get; set; }
    }
}
