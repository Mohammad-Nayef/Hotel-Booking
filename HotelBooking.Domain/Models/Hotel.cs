using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class Hotel : Entity
    {
        public string Name { get; set; }
        public string BriefDescription { get; set; }
        public string FullDescription { get; set; }
        public float StarRating { get; set; }
        public string OwnerName { get; set; }
        public List<HotelReview> Reviews { get; set; }
        public Location Location { get; set; }
        public List<Image> Images { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Discount> Discounts { get; set; }
        public List<Visit> Visits { get; set; }
    }
}
