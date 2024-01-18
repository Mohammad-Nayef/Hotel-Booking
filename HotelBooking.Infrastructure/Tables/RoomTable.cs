namespace HotelBooking.Infrastructure.Tables
{
    internal class RoomTable : DbEntity
    {
        public double Number { get; set; }
        public string Type { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public string BriefDescription { get; set; }
        public decimal PricePerNight { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Guid HotelId { get; set; }
        public HotelTable Hotel { get; set; }
        public List<ImageTable> Images { get; } = new();
        public List<CartItemTable> CartItems { get; } = new();
        public List<BookingTable> Bookings { get; } = new();
    }
}
