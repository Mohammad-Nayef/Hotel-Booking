namespace HotelBooking.Db.Tables
{
    internal class HotelTable : DbEntity
    {
        public string Name { get; set; }
        public string BriefDescription { get; set; }
        public string FullDescription { get; set; }
        public float StarRating { get; set; }
        public string OwnerName { get; set; }
        public string Geolocation { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public List<HotelReviewTable> Reviews { get; } = new();
        public Guid CityId { get; set; }
        public CityTable City { get; set; }
        public List<ImageTable> Images { get; } = new();
        public List<RoomTable> Rooms { get; } = new();
        public List<DiscountTable> Discounts { get; } = new();
        public List<VisitTable> Visits { get; } = new();
    }
}
