namespace HotelBooking.Infrastructure.Tables
{
    internal class BookingTable : DbEntity
    {
        public DateTime CreationDate { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public decimal Price { get; set; }
        public Guid RoomId { get; set; }
        public RoomTable Room { get; set; }
        public Guid UserId { get; set; }
        public UserTable User { get; set; }
    }
}
