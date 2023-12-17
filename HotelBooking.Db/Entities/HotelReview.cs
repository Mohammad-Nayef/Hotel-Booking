namespace HotelBooking.Db.Entities
{
    public class HotelReview
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
