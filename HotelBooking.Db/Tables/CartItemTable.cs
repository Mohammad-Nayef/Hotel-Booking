namespace HotelBooking.Db.Tables
{
    internal class CartItemTable : DbEntity
    {
        public DateTime AddingDate { get; set; }
        public Guid RoomId { get; set; }
        public RoomTable Room { get; set; }
        public Guid UserId { get; set; }
        public UserTable User { get; set; }
    }
}
