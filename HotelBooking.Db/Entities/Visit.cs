namespace HotelBooking.Db.Entities
{
    public class Visit
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; }
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
