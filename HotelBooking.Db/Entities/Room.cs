namespace HotelBooking.Db.Entities
{
    public class Room
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double RoomNumber { get; set; }
        public string Type { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public string BriefDescription { get; set; }
        public decimal PricePerNight { get; set; }
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public List<Image> Images { get; } = new();
        public List<CartItem> CartItems { get; } = new();
        public List<Booking> Bookings { get; } = new();
    }
}
