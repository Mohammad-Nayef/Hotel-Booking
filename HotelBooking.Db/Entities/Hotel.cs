namespace HotelBooking.Db.Entities
{
    public class Hotel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string BriefDescription { get; set; }
        public string FullDescription { get; set; }
        public float StarRating { get; set; }
        public string OwnerName { get; set; }
        public List<HotelReview> Reviews { get; } = new();
        public Location Location { get; set; }
        public List<Image> Images { get; } = new();
        public List<Room> Rooms { get; } = new();
        public List<Discount> Discounts { get; } = new();
        public List<Visit> Visits { get; } = new();
    }
}
