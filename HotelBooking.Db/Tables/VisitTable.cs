namespace HotelBooking.Db.Tables
{
    internal class VisitTable : DbEntity
    {
        public DateTime Date { get; set; }
        public Guid HotelId { get; set; }
        public HotelTable Hotel { get; set; }
        public Guid UserId { get; set; }
        public UserTable User { get; set; }
    }
}
